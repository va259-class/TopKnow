<script>
import { mapStores } from 'pinia'
import { useQuizStore } from '@/stores/quiz'
import { RotateCcw, Home, Award } from 'lucide-vue-next'

export default {
  name: 'ResultView',
  components: {
    RotateCcw,
    Home,
    Award,
  },
  computed: {
    ...mapStores(useQuizStore),
    accuracy() {
      const total = this.quizStore.stats.correct + this.quizStore.stats.wrong
      if (total === 0) return 0
      return Math.round((this.quizStore.stats.correct / total) * 100)
    },
  },
  methods: {
    playAgain() {
      this.quizStore.startGame()
      this.$router.push('/quiz')
    },
    returnHome() {
      this.$router.push('/')
    },
  },
}
</script>

<template>
  <div class="min-h-screen flex items-center justify-center p-4">
    <div
      class="max-w-2xl w-full bg-surface/90 backdrop-blur-xl rounded-3xl border border-white/10 p-8 md:p-12 shadow-2xl relative overflow-hidden text-center z-10"
    >
      <!-- Glow Effect Behind Stats -->
      <div
        class="absolute top-0 left-1/2 -translate-x-1/2 w-full h-1/2 bg-gradient-to-b from-primary/20 to-transparent filter blur-[60px] pointer-events-none"
      ></div>

      <Award
        class="w-20 h-20 md:w-24 md:h-24 text-warning mx-auto mb-6 text-glow-primary animate-[bounce_2s_infinite]"
      />

      <h1 class="text-4xl md:text-5xl font-bold text-text-light drop-shadow-lg mb-2">
        Maç Tamamlandı
      </h1>
      <p class="text-lg text-text-light/70 mb-10 font-medium tracking-wide">
        Arena Simülasyonu Sona Erdi
      </p>

      <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-12">
        <div class="bg-bg-darker/60 rounded-2xl p-6 border border-white/5 relative group">
          <div
            class="absolute inset-0 bg-primary/5 opacity-0 group-hover:opacity-100 transition-opacity rounded-2xl"
          ></div>
          <p class="text-sm uppercase tracking-widest text-text-light/50 font-bold mb-2">
            Skorunuz
          </p>
          <p
            class="text-4xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-primary to-blue-400"
          >
            {{ quizStore.score }}
          </p>
        </div>

        <div class="bg-bg-darker/60 rounded-2xl p-6 border border-white/5 relative group">
          <div
            class="absolute inset-0 bg-success/5 opacity-0 group-hover:opacity-100 transition-opacity rounded-2xl"
          ></div>
          <p class="text-sm uppercase tracking-widest text-text-light/50 font-bold mb-2">
            Doğruluk Oranı
          </p>
          <p class="text-4xl font-bold text-success">{{ accuracy }}%</p>
        </div>

        <div class="bg-bg-darker/60 rounded-2xl p-6 border border-white/5 relative group">
          <div
            class="absolute inset-0 bg-danger/5 opacity-0 group-hover:opacity-100 transition-opacity rounded-2xl"
          ></div>
          <p class="text-sm uppercase tracking-widest text-text-light/50 font-bold mb-2">
            Cevaplar
          </p>
          <div class="flex items-center justify-center space-x-3 text-2xl font-bold">
            <span class="text-success">{{ quizStore.stats.correct }}</span>
            <span class="text-text-light/30">/</span>
            <span class="text-danger">{{ quizStore.stats.wrong }}</span>
          </div>
        </div>
      </div>

      <div class="flex flex-col sm:flex-row gap-4 justify-center">
        <button
          @click="playAgain"
          class="flex items-center justify-center space-x-2 px-8 py-4 bg-primary text-text-light font-bold rounded-xl transition-all duration-300 hover:-translate-y-1 hover:box-glow-primary active:scale-95"
        >
          <RotateCcw class="w-5 h-5" />
          <span>Tekrar Oyna</span>
        </button>

        <button
          @click="returnHome"
          class="flex items-center justify-center space-x-2 px-8 py-4 bg-surface border-2 border-primary/30 text-text-light rounded-xl font-bold transition-all duration-300 hover:border-primary/80 hover:bg-surface/80 active:scale-95 group"
        >
          <Home class="w-5 h-5 group-hover:-translate-y-0.5 transition-transform" />
          <span>Anasayfaya Dön</span>
        </button>
      </div>
    </div>
  </div>
</template>
