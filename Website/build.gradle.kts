plugins {
    java
    kotlin("jvm") version "1.3.41"
}

repositories {
    jcenter()
}

version = "0.0.0"

dependencies {
    // kotlin runtime
    compile(group = "org.jetbrains.kotlin", name = "kotlin-stdlib", version = "1.3.41")
    compile(group = "org.jetbrains.kotlin", name = "kotlin-stdlib-jdk8", version = "1.3.41")
    compile(group = "org.jetbrains.kotlin", name = "kotlin-reflect", version = "1.3.41")
    compile(group = "org.jetbrains", name = "annotations", version = "17.0.0")

    // jackson, for parsing json
    compile(group = "com.fasterxml.jackson.core", name = "jackson-databind", version = "2.10.0")
    // javalin, for wrapping servlet and serving content
    compile(group = "io.javalin", name = "javalin", version = "3.5.0")
    // jedis, for bridging redis.
    compile(group = "redis.clients", name = "jedis", version = "3.1.0")
    // slf4j, with java.util.logging binding
    compile(group = "org.slf4j", name = "slf4j-jdk14", version = "1.7.27")
}

tasks.register<Jar>("shadeJar") {
    dependsOn(":jar")
    classifier = "shaded"
    from(sourceSets["main"].output)
    from(configurations.compile.map {
        if (it.isDirectory) it else zipTree(it).matching {
            exclude("META-INF/")
        }
    })
}

artifacts {
    add("archives", tasks["shadeJar"])
}
