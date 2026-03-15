<script>
export default {
  name: 'ScoreBadge',
  props: {
    score: {
      type: Number,
      required: true,
    },
  },
}
</script>

<template>
  <div
    class="flex items-center space-x-3 px-6 py-3 bg-surface/90 backdrop-blur-md rounded-2xl border border-primary/30 box-glow-primary"
  >
    <div class="w-2 h-2 rounded-full bg-primary animate-pulse-fast"></div>
    <span class="text-sm text-text-light/70 uppercase tracking-wider font-semibold">Skor</span>
    <span class="text-2xl font-bold text-text-light font-mono">{{ score }}</span>
  </div>
</template>
