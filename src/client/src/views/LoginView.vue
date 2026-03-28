<script>
import { mapStores } from 'pinia'
import { useAuthStore } from '@/stores/auth'
import { LogIn } from 'lucide-vue-next'

export default {
  name: 'LoginView',
  components: { LogIn },
  data() {
    return {
      mail: '',
      password: '',
      error: '',
      loading: false,
    }
  },
  computed: {
    ...mapStores(useAuthStore),
  },
  methods: {
    async submit() {
      this.error = ''
      this.loading = true
      try {
        await this.authStore.login(this.mail.trim(), this.password)
        const redirect = this.$route.query.redirect || '/'
        await this.$router.replace(redirect)
      } catch (e) {
        this.error = e.response?.data?.message || e.message || 'Giriş başarısız'
      } finally {
        this.loading = false
      }
    },
  },
}
</script>

<template>
  <div class="w-full min-h-screen flex flex-col items-center justify-center px-4 py-12">
    <div
      class="w-full max-w-md bg-surface/80 backdrop-blur-md rounded-3xl border border-white/10 p-8 shadow-2xl"
    >
      <div class="flex items-center gap-3 mb-8">
        <LogIn class="w-8 h-8 text-primary" />
        <h1 class="text-2xl font-bold tracking-tight">Giriş</h1>
      </div>

      <form class="space-y-5" @submit.prevent="submit">
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">E-posta</label>
          <input
            v-model="mail"
            type="email"
            required
            autocomplete="email"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50"
          />
        </div>
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">Şifre</label>
          <input
            v-model="password"
            type="password"
            required
            autocomplete="current-password"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50"
          />
        </div>

        <p v-if="error" class="text-sm text-danger">{{ error }}</p>

        <button
          type="submit"
          :disabled="loading"
          class="w-full py-3 rounded-xl bg-primary hover:bg-primary/90 text-text-light font-semibold transition disabled:opacity-50"
        >
          {{ loading ? '…' : 'Giriş yap' }}
        </button>
      </form>

      <p class="mt-6 text-center text-sm text-text-light/60">
        Hesabın yok mu?
        <router-link to="/register" class="text-primary hover:underline">Kayıt ol</router-link>
      </p>
    </div>
  </div>
</template>
