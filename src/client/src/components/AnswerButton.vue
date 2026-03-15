<script>
export default {
  name: 'AnswerButton',
  props: {
    text: {
      type: String,
      required: true,
    },
    state: {
      type: String,
      default: 'default', // 'default', 'selected', 'correct', 'wrong'
      validator: (value) => ['default', 'selected', 'correct', 'wrong'].includes(value),
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  emits: ['click'],
  computed: {
    buttonClasses() {
      const base =
        'w-full min-h-[80px] p-6 text-lg md:text-xl font-medium rounded-2xl transition-all duration-300 transform border-2'

      if (this.disabled && this.state === 'default') {
        return `${base} bg-surface/50 border-surface text-text-light/50 cursor-not-allowed`
      }

      switch (this.state) {
        case 'selected':
          return `${base} bg-primary/20 border-primary text-text-light box-glow-primary scale-[1.02]`
        case 'correct':
          return `${base} bg-success/20 border-success text-success box-glow-success scale-[1.05]`
        case 'wrong':
          return `${base} bg-danger/20 border-danger text-danger box-glow-danger animate-shake`
        default:
          return `${base} bg-surface/80 border-surface hover:border-primary/50 text-text-light hover:bg-surface hover:-translate-y-1 hover:box-glow-primary active:scale-95 cursor-pointer backdrop-blur-sm`
      }
    },
  },
  methods: {
    handleClick() {
      this.$emit('click')
    },
  },
}
</script>

<template>
  <button :class="buttonClasses" :disabled="disabled" @click="handleClick">
    {{ text }}
  </button>
</template>
