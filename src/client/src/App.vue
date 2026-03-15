<script>
import { RouterView } from 'vue-router'
import AnimatedBackground from '@/components/AnimatedBackground.vue'

export default {
  name: 'App',
  components: {
    RouterView,
    AnimatedBackground,
  },
}
</script>

<template>
  <div class="min-h-screen relative w-full overflow-hidden">
    <AnimatedBackground />
    <main class="relative z-10 w-full min-h-screen">
      <RouterView />
    </main>
  </div>
</template>
