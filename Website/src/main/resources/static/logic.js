var app = new Vue({
  el: '#app',
  data: {
    status: 'Waiting for your answer…',
    question: null,
    correctAnswer: null,
    userAnswer: '',
    next: false,
    startAt: -1,
    endAt: -1,
    memes: [
      "A computer could not possibly be this slow! You pass the Turing Test!",
      "You humans seem awfully slow. What Operating System are you using?",
      "Silly human, your runtime complexity went from n^0 to n^100 real fast.",
      "I can do a million floating point operations per second, how about you?",
      "Looks like it's another victory for the machines.",
      "Is your code running? Better go catch it!",
      "Lazy human, do you even compute?"
    ]
  },
  watch: {
    userAnswer: function (newAns, oldAns) {
      this.debouncedCheck(newAns);
    }
  },
  methods: {
    reload: function(event) {
      this.getQuestion();
      document.getElementById("user-input").readOnly = false;
    }
  },
  created: function() {
    this.debouncedCheck = _.debounce(ans => {
      if (ans == this.correctAnswer) {
        this.status = "Correct! Your time cost: " + ((Date.now() - this.startAt) / 1000) + " seconds";
        document.getElementById("user-input").readOnly = true
        this.endAt = Date.now();
        this.next = true;
      } else if (ans == "") {
        this.status = "Waiting for your answer…";
      } else {
        let userLines = ans.split("\n");
        let correctLines = this.correctAnswer.split("\n");
        let l = 0;
        for (; l < userLines.length; l++) {
          if (userLines[l] != correctLines[l]) {
            break;
          }
        }
        if (l >= userLines.length) {
          this.status = "That doesn't sound right… did you omit lines?"
        } else {
          this.status = "That doesn't sound right… check your line #" + (l + 1) + "?"
        }
      }
    }, 100);
    this.getQuestion = () => {
      fetch("api/").then(data => data.json()).then(data => {
        this.question = data.question;
        this.correctAnswer = data.answer;
        this.status = 'Waiting for your answer…';
        this.userAnswer = '';
        this.next = false;
        this.startAt = Date.now();
        this.endAt = -1;
      })
    };
    this.getQuestion();
    setInterval(() => {
      if (this.next && Date.now() - this.endAt > 5000) {
        this.getQuestion();
        document.getElementById("user-input").readOnly = false;
      }
    }, 500)
  }
})