<script>
import { mapStores } from 'pinia'
import { useQuizStore } from '@/stores/quiz'
import { Loader2 } from 'lucide-vue-next'

export default {
  name: 'LobbyView',
  components: {
    Loader2,
  },
  data() {
    return {
      isLoading: true,
    }
  },
  computed: {
    ...mapStores(useQuizStore),
  },
  async mounted() {
    // Fetch questions from API immediately
    await this.quizStore.fetchQuestions()

    // Artificial delay for "Lobby" feel
    setTimeout(() => {
      this.isLoading = false
      // Start game logic
      this.quizStore.startGame()
      this.$router.push('/quiz')
    }, 3000)
  },
}
</script>

<template>
  <div class="min-h-screen flex flex-col items-center justify-center p-4">
    <div
      class="max-w-md w-full bg-surface/80 backdrop-blur-md rounded-3xl border border-white/10 p-10 text-center shadow-2xl relative overflow-hidden"
    >
      <!-- Background subtle animated pulse -->
      <div
        class="absolute inset-0 bg-gradient-to-br from-primary/10 to-success/10 animate-[pulse_2s_ease-in-out_infinite]"
      ></div>

      <div class="relative z-10">
        <h2 class="text-3xl font-bold mb-8 text-text-light drop-shadow-md">
          Arenaya Bağlanıyor...
        </h2>

        <div class="flex justify-center items-center mb-8 h-24">
          <Loader2
            class="w-16 h-16 text-primary animate-spin filter drop-shadow-[0_0_10px_rgba(99,102,241,0.8)]"
          />
        </div>

        <div class="space-y-3">
          <p class="text-text-light/60 font-medium">
            Rakipler aranıyor... <span class="text-success inline-block ml-2">Eşleşti!</span>
          </p>
          <p class="text-text-light/60 font-medium">
            Sorular yükleniyor...
            <span class="text-success inline-block ml-2">{{
              quizStore.questions.length > 0 ? 'Tamamlandı!' : 'Bekleniyor...'
            }}</span>
          </p>
          <p class="text-text-light/60 font-medium animate-pulse text-lg mt-4">Hazırlanın!</p>
        </div>
      </div>
    </div>
  </div>
</template>
