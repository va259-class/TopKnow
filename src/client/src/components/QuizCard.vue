<script>
export default {
  name: 'QuizCard',
  props: {
    question: {
      type: String,
      required: true,
    },
  },
}
</script>

<template>
  <div
    class="w-full max-w-4xl mx-auto p-8 md:p-12 mb-8 bg-surface/80 backdrop-blur-md border border-white/5 rounded-3xl shadow-2xl">
    <h2 class="text-2xl md:text-3xl lg:text-4xl font-bold text-text-light text-center leading-relaxed">
      {{ question.question }}
    </h2>
  </div>
</template>
