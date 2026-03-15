<script>
export default {
  name: 'AnimatedBackground',
}
</script>

<template>
  <div class="fixed inset-0 z-0 bg-bg-darker overflow-hidden">
    <!-- Base dark gradient -->
    <div class="absolute inset-0 bg-gradient-to-br from-bg-darker to-bg-dark opacity-90"></div>

    <!-- Moving abstract glow orbs -->
    <div
      class="absolute -top-40 -left-40 w-96 h-96 bg-primary rounded-full mix-blend-multiply filter blur-[128px] opacity-20 animate-[pulse_6s_ease-in-out_infinite]"
    ></div>
    <div
      class="absolute top-1/2 -right-40 w-96 h-96 bg-success rounded-full mix-blend-multiply filter blur-[128px] opacity-10 animate-[pulse_8s_ease-in-out_infinite_2s]"
    ></div>
    <div
      class="absolute -bottom-40 left-1/3 w-96 h-96 bg-primary rounded-full mix-blend-multiply filter blur-[128px] opacity-20 animate-[pulse_7s_ease-in-out_infinite_4s]"
    ></div>

    <!-- Glowing grid overlay -->
    <div
      class="absolute inset-0 bg-[linear-gradient(rgba(255,255,255,0.03)_1px,transparent_1px),linear-gradient(90deg,rgba(255,255,255,0.03)_1px,transparent_1px)] bg-[size:64px_64px] [mask-image:radial-gradient(ellipse_80%_80%_at_50%_50%,#000_10%,transparent_100%)]"
    ></div>
  </div>
</template>
