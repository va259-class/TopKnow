<script>
export default {
  name: 'ProgressBar',
  props: {
    current: {
      type: Number,
      required: true,
    },
    total: {
      type: Number,
      required: true,
    },
  },
  computed: {
    percentage() {
      if (this.total === 0) return 0
      return (this.current / this.total) * 100
    },
  },
}
</script>

<template>
  <div class="w-full flex items-center justify-between space-x-4 mb-8">
    <div class="flex-1 h-3 bg-surface rounded-full overflow-hidden shadow-inner">
      <div
        class="h-full bg-primary transition-all duration-500 ease-out box-glow-primary rounded-full"
        :style="{ width: `${percentage}%` }"
      ></div>
    </div>
    <span class="text-text-light font-medium whitespace-nowrap">
      S: {{ current }} / {{ total }}
    </span>
  </div>
</template>
