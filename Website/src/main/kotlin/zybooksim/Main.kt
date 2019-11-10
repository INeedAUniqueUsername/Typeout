package zybooksim

import io.javalin.Javalin
import io.javalin.plugin.json.JavalinJson
import redis.clients.jedis.Jedis
import java.util.*

fun main(vararg args: String) {
    val redis = Jedis("localhost", try {
        args[1].toInt()
    } catch (e: Exception) {
        6379
    })
    redis.ping()
    Javalin.create { config ->
        config.showJavalinBanner = false
        config.addStaticFiles("/static")
    }.start(try {
        args[0].toInt()
    } catch (e: Exception) {
        8080
    }).apply {
        this.get("api/") { ctx ->
            val randomQuestion: String? = redis.randomKey()
            if (randomQuestion != null) {
                ctx.status(200).json(redis.hgetAll(randomQuestion))
            } else {
                ctx.status(500).result("There is no question on the server")
            }
        }
        this.put("api/question") { ctx ->
            try {
                val id = UUID.randomUUID().toString()
                JavalinJson.fromJson(ctx.body(), Map::class.java).forEach { (key, value) ->
                    try {
                        redis.hset(id, key.toString(), value.toString())
                    } catch (setFail: Exception) {
                        println(ctx.body())
                        println()
                    }
                }
                ctx.status(201).result("Success")
            } catch (parseFail: Exception) {
                ctx.status(400).result("Malformed payload")
            }
        }
    }
}