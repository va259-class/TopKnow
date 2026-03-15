<script>
export default {
  name: 'TimerCircle',
  props: {
    timeLeft: {
      type: Number,
      required: true,
    },
    maxTime: {
      type: Number,
      default: 15,
    },
  },
  data() {
    return {
      radius: 40,
    }
  },
  computed: {
    circumference() {
      return 2 * Math.PI * this.radius
    },
    dashOffset() {
      return this.circumference - (this.timeLeft / this.maxTime) * this.circumference
    },
    isDanger() {
      return this.timeLeft <= 5
    },
  },
}
</script>

<template>
  <div class="relative flex items-center justify-center w-24 h-24">
    <svg class="w-full h-full -rotate-90 transform" viewBox="0 0 100 100">
      <!-- Background circle -->
      <circle cx="50" cy="50" :r="radius" class="stroke-surface fill-none" stroke-width="8" />
      <!-- Progress circle -->
      <circle
        cx="50"
        cy="50"
        :r="radius"
        class="fill-none transition-all duration-1000 ease-linear"
        :stroke-dasharray="circumference"
        :stroke-dashoffset="-dashOffset"
        stroke-linecap="round"
        stroke-width="8"
        :class="[
          isDanger
            ? 'stroke-danger animate-pulse-fast box-glow-danger'
            : 'stroke-primary text-glow-primary',
        ]"
      />
    </svg>
    <!-- Time Text -->
    <div class="absolute flex flex-col items-center justify-center">
      <span
        class="text-2xl font-bold font-mono transition-colors duration-300"
        :class="isDanger ? 'text-danger' : 'text-text-light'"
      >
        {{ timeLeft }}
      </span>
    </div>
  </div>
</template>
