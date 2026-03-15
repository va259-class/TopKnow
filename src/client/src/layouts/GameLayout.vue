<script>
import { mapStores } from 'pinia'
import { useQuizStore } from '@/stores/quiz'

import AnimatedBackground from '@/components/AnimatedBackground.vue'
import ProgressBar from '@/components/ProgressBar.vue'
import ScoreBadge from '@/components/ScoreBadge.vue'
import TimerCircle from '@/components/TimerCircle.vue'

export default {
  name: 'GameLayout',
  components: {
    AnimatedBackground,
    ProgressBar,
    ScoreBadge,
    TimerCircle,
  },
  computed: {
    ...mapStores(useQuizStore),
    playerName() {
      // Can be integrated with auth later
      return 'Player 1'
    },
  },
}
</script>

<template>
  <div class="w-full flex flex-col min-h-screen px-4 py-6 md:px-8 lg:px-12 max-w-7xl mx-auto">
    <!-- Header -->
    <header class="flex justify-between items-center mb-8 relative z-20">
      <div class="flex items-center space-x-4">
        <div class="w-12 h-12 rounded-full bg-gradient-to-br from-primary to-success p-0.5">
          <div
            class="w-full h-full bg-surface rounded-full flex items-center justify-center font-bold text-xl"
          >
            {{ playerName.charAt(0) }}
          </div>
        </div>
        <div>
          <h2 class="font-bold text-lg text-text-light drop-shadow-md">{{ playerName }}</h2>
          <p class="text-xs text-primary font-medium tracking-widest uppercase">Challenger</p>
        </div>
      </div>

      <ScoreBadge :score="quizStore.score" />
    </header>

    <!-- Main Content -->
    <main class="flex-grow flex flex-col justify-center relative z-20">
      <ProgressBar :current="quizStore.currentQuestionNumber" :total="quizStore.totalQuestions" />

      <div class="relative w-full">
        <!-- Allows slotting the QuizCard / Answers -->
        <slot></slot>
      </div>
    </main>

    <!-- Footer -->
    <footer class="mt-8 flex justify-between items-end relative z-20">
      <div class="text-sm font-medium text-text-light/50 pb-2">
        Soru {{ quizStore.currentQuestionNumber }} / {{ quizStore.totalQuestions }}
      </div>

      <div class="pb-2">
        <TimerCircle :time-left="quizStore.timer" />
      </div>
    </footer>
  </div>
</template>
