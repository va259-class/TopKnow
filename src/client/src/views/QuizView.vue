<script>
import { mapState, mapActions, mapStores } from 'pinia'
import { useQuizStore } from '@/stores/quiz'
import { useHubStore } from '@/stores/hub'

import GameLayout from '@/layouts/GameLayout.vue'
import QuizCard from '@/components/QuizCard.vue'
import AnswerButton from '@/components/AnswerButton.vue'

export default {
  name: 'QuizView',
  components: {
    GameLayout,
    QuizCard,
    AnswerButton,
  },
  computed: {
    ...mapState(useQuizStore, ['currentQuestion', 'currentQuestionIndex', 'isAnswerLocked', 'isGameOver']),
    ...mapStores(useHubStore)
  },
  methods: {
    ...mapActions(useQuizStore, ['getAnswerState', 'selectAnswer']),
    getReady() {
      this.hubStore.isReady(this.$route.params.id);
    }
  },
  watch: {
    isGameOver: {
      handler(newVal) {
        if (newVal) {
          this.$router.push('/result')
        }
      },
      immediate: true,
    },
  },
}
</script>

<template>
  <GameLayout v-if="currentQuestion">
    <!-- Transition for entering/leaving questions -->
    <transition name="fade-slide" mode="out-in">
      <div class="w-full relative">
        <QuizCard :question="currentQuestion" />

        <div class="grid grid-cols-1 md:grid-cols-2 gap-4 md:gap-6 mt-8 w-full max-w-4xl mx-auto">
          <AnswerButton v-for="(answer, index) in currentQuestion.answers" :key="index" :text="answer"
            :state="getAnswerState(index)" :disabled="isAnswerLocked" @click="selectAnswer(index)" />
        </div>
      </div>
    </transition>
  </GameLayout>
  <!-- Optional loading empty state -->
  <div v-else class="min-h-screen flex items-center justify-center text-text-light">
    <div class="animate-pulse">
      <button
        class="w-full py-3 px-3 rounded-xl bg-primary hover:bg-primary/90 text-text-light font-semibold transition disabled:opacity-50"
        @click="getReady">Hazırım</button>
    </div>
  </div>
</template>

<style scoped>
.fade-slide-enter-active,
.fade-slide-leave-active {
  transition: all 0.5s ease;
}

.fade-slide-enter-from {
  opacity: 0;
  transform: translateY(30px);
}

.fade-slide-leave-to {
  opacity: 0;
  transform: translateY(-30px);
}
</style>
