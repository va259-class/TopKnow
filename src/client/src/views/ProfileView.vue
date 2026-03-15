<script>
import { User, Trophy, Star, Clock, ArrowLeft } from 'lucide-vue-next'

export default {
  name: 'ProfileView',
  components: {
    User,
    Trophy,
    Star,
    Clock,
    ArrowLeft,
  },
  data() {
    return {
      // Mock data for profile stats
      userStats: {
        username: 'Challenger_One',
        rank: 'Diamond Elite',
        totalMatches: 142,
        winRate: '68%',
        highestScore: 12500,
        playTime: '45h 20m',
      },
    }
  },
}
</script>

<template>
  <div class="min-h-screen flex flex-col items-center p-4 pt-12">
    <div class="w-full max-w-4xl relative z-20">
      <!-- Top Navigation Menu -->
      <button
        @click="router.push('/')"
        class="flex items-center space-x-2 text-text-light/70 hover:text-primary transition-colors mb-8 group"
      >
        <ArrowLeft class="w-5 h-5 group-hover:-translate-x-1 transition-transform" />
        <span class="font-medium tracking-wide">Ana Menüye Dön</span>
      </button>

      <!-- Profile Header -->
      <div
        class="bg-surface/80 backdrop-blur-md rounded-3xl p-8 border border-white/10 mb-8 flex flex-col md:flex-row items-center md:space-x-8 shadow-2xl relative overflow-hidden"
      >
        <div
          class="absolute top-0 right-0 w-64 h-64 bg-primary/10 rounded-full filter blur-[80px] -z-10 animate-pulse"
        ></div>

        <div
          class="w-32 h-32 rounded-full bg-gradient-to-tr from-primary to-success p-1 mb-6 md:mb-0 shrink-0"
        >
          <div
            class="w-full h-full bg-bg-darker rounded-full flex flex-col items-center justify-center relative overflow-hidden group cursor-pointer"
          >
            <div
              class="absolute inset-0 bg-primary/20 opacity-0 group-hover:opacity-100 transition-opacity"
            ></div>
            <User
              class="w-12 h-12 text-text-light/80 mb-1 group-hover:text-primary transition-colors"
            />
            <span class="text-xs font-bold text-text-light/50 group-hover:text-text-light/90"
              >DÜZENLE</span
            >
          </div>
        </div>

        <div class="text-center md:text-left">
          <h1
            class="text-3xl md:text-4xl font-bold text-text-light mb-2 flex items-center justify-center md:justify-start"
          >
            {{ userStats.username }}
            <Trophy class="w-6 h-6 ml-3 text-warning text-glow-primary" />
          </h1>
          <p class="text-primary font-semibold tracking-wider uppercase mb-4">
            {{ userStats.rank }}
          </p>
          <div class="flex flex-wrap items-center justify-center md:justify-start gap-4">
            <span
              class="px-4 py-1.5 rounded-full bg-white/5 border border-white/10 text-sm font-medium text-text-light/80"
            >
              Sv. 42
            </span>
            <span
              class="px-4 py-1.5 rounded-full bg-success/10 border border-success/20 text-sm font-medium text-success"
            >
              Çevrimiçi
            </span>
          </div>
        </div>
      </div>

      <!-- Stats Grid -->
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
        <div
          class="bg-surface/60 backdrop-blur-sm rounded-2xl p-6 border border-white/5 hover:bg-surface/80 transition-colors shadow-lg group"
        >
          <div class="flex justify-between items-start mb-4">
            <h3 class="text-sm tracking-widest text-text-light/50 font-bold uppercase">Maçlar</h3>
            <div
              class="p-2 rounded-lg bg-primary/10 text-primary group-hover:scale-110 transition-transform"
            >
              <User class="w-4 h-4" />
            </div>
          </div>
          <p class="text-3xl font-bold text-text-light">{{ userStats.totalMatches }}</p>
        </div>

        <div
          class="bg-surface/60 backdrop-blur-sm rounded-2xl p-6 border border-white/5 hover:bg-surface/80 transition-colors shadow-lg group"
        >
          <div class="flex justify-between items-start mb-4">
            <h3 class="text-sm tracking-widest text-text-light/50 font-bold uppercase">
              Kazanma Oranı
            </h3>
            <div
              class="p-2 rounded-lg bg-success/10 text-success group-hover:scale-110 transition-transform"
            >
              <Trophy class="w-4 h-4" />
            </div>
          </div>
          <p
            class="text-3xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-success to-emerald-400"
          >
            {{ userStats.winRate }}
          </p>
        </div>

        <div
          class="bg-surface/60 backdrop-blur-sm rounded-2xl p-6 border border-white/5 hover:bg-surface/80 transition-colors shadow-lg group"
        >
          <div class="flex justify-between items-start mb-4">
            <h3 class="text-sm tracking-widest text-text-light/50 font-bold uppercase">
              En Yüksek Skor
            </h3>
            <div
              class="p-2 rounded-lg bg-warning/10 text-warning group-hover:scale-110 transition-transform"
            >
              <Star class="w-4 h-4" />
            </div>
          </div>
          <p class="text-3xl font-bold text-warning">
            {{ userStats.highestScore.toLocaleString('tr-TR') }}
          </p>
        </div>

        <div
          class="bg-surface/60 backdrop-blur-sm rounded-2xl p-6 border border-white/5 hover:bg-surface/80 transition-colors shadow-lg group"
        >
          <div class="flex justify-between items-start mb-4">
            <h3 class="text-sm tracking-widest text-text-light/50 font-bold uppercase">
              Oynama Süresi
            </h3>
            <div
              class="p-2 rounded-lg bg-blue-400/10 text-blue-400 group-hover:scale-110 transition-transform"
            >
              <Clock class="w-4 h-4" />
            </div>
          </div>
          <p class="text-3xl font-bold text-text-light">{{ userStats.playTime }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
