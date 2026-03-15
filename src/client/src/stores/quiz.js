import { defineStore } from 'pinia'
import api from '../services/api'

export const useQuizStore = defineStore('quiz', {
  state: () => ({
    questions: [],
    currentQuestionIndex: 0,
    score: 0,
    isPlaying: false,
    isGameOver: false,
    timer: 15,
    timerInterval: null,
    selectedAnswerIndex: null,
    isAnswerLocked: false,
    stats: {
      correct: 0,
      wrong: 0,
    },
  }),
  getters: {
    currentQuestion: (state) => state.questions[state.currentQuestionIndex],
    totalQuestions: (state) => state.questions.length,
    currentQuestionNumber: (state) => state.currentQuestionIndex + 1,
  },
  actions: {
    async fetchQuestions() {
      this.questions = await api.getQuestions()
    },
    startGame() {
      this.currentQuestionIndex = 0
      this.score = 0
      this.stats = { correct: 0, wrong: 0 }
      this.isPlaying = true
      this.isGameOver = false
      this.resetTurn()
      this.startTimer()
    },
    startTimer() {
      this.stopTimer()
      this.timer = 15
      this.timerInterval = setInterval(() => {
        if (this.timer > 0) {
          this.timer--
        } else {
          // Time is up
          this.handleTimeUp()
        }
      }, 1000)
    },
    stopTimer() {
      if (this.timerInterval) {
        clearInterval(this.timerInterval)
        this.timerInterval = null
      }
    },
    handleTimeUp() {
      this.stopTimer()
      this.isAnswerLocked = true
      this.selectedAnswerIndex = -1 // Indicates timeout
      this.stats.wrong++

      // Auto proceed after delay
      setTimeout(() => {
        this.nextQuestion()
      }, 2500)
    },
    selectAnswer(index) {
      if (this.isAnswerLocked) return

      this.stopTimer()
      this.selectedAnswerIndex = index
      this.isAnswerLocked = true

      const isCorrect = index === this.currentQuestion.correctIndex

      if (isCorrect) {
        // Calculate score based on time left (max 150 points base per question + time bonus)
        const basePoints = 100
        const timeBonus = this.timer * 10
        this.score += basePoints + timeBonus
        this.stats.correct++
      } else {
        this.stats.wrong++
      }

      // Auto proceed to next question after result shown
      setTimeout(() => {
        this.nextQuestion()
      }, 2500)
    },
    nextQuestion() {
      if (this.currentQuestionIndex < this.totalQuestions - 1) {
        this.currentQuestionIndex++
        this.resetTurn()
        this.startTimer()
      } else {
        this.finishQuiz()
      }
    },
    resetTurn() {
      this.selectedAnswerIndex = null
      this.isAnswerLocked = false
    },
    finishQuiz() {
      this.stopTimer()
      this.isPlaying = false
      this.isGameOver = true
    },
    getAnswerState(index) {
      if (!this.isAnswerLocked) {
        return this.selectedAnswerIndex === index ? 'selected' : 'default'
      }

      const isCorrectAnswer = index === this.currentQuestion.correctIndex

      if (isCorrectAnswer) return 'correct'
      if (this.selectedAnswerIndex === index) return 'wrong'
      return 'default'
    },
  },
})
