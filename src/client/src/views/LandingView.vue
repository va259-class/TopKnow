<script>
import { PlusCircle } from 'lucide-vue-next'

export default {
  name: 'LandingView',
  components: {
    PlusCircle,
  },
  methods: {
    startLobby() {
      this.$router.push('/lobby')
    },
  },
}
</script>

<template>
  <div class="w-full min-h-screen flex flex-col items-center justify-center px-4">
    <div class="text-center relative z-20 max-w-3xl mx-auto">
      <!-- Glow effect behind title -->
      <div
        class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 w-[120%] h-40 bg-primary/20 filter blur-[80px] rounded-full z-0 pointer-events-none"
      ></div>

      <h1 class="relative z-10 text-6xl md:text-8xl font-bold mb-6 tracking-tighter">
        <span
          class="text-transparent bg-clip-text bg-gradient-to-r from-primary via-blue-400 to-success text-glow-primary"
        >
          TopKnow
        </span>
      </h1>

      <p
        class="relative z-10 text-xl md:text-2xl text-text-light/80 mb-12 font-light leading-relaxed"
      >
        Arenaya girin. Bilginizi test edin.<br />Liderlik tablosuna hükmedin.
      </p>

      <button
        @click="startLobby"
        class="relative z-10 group inline-flex items-center justify-center space-x-3 px-8 py-4 bg-primary/90 hover:bg-primary text-text-light font-bold rounded-full text-xl transition-all duration-300 transform hover:scale-105 hover:box-glow-primary active:scale-95 overflow-hidden"
      >
        <!-- Overlay gradient for hover effect -->
        <div
          class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-1000"
        ></div>

        <span class="tracking-wide">ARENAYA GİR</span>
        <PlusCircle class="w-6 h-6 group-hover:rotate-90 transition-transform duration-300" />
      </button>
    </div>
  </div>
</template>
