<script>
import { mapStores } from 'pinia'
import { useAuthStore } from '@/stores/auth'
import { UserPlus } from 'lucide-vue-next'

export default {
  name: 'RegisterView',
  components: { UserPlus },
  data() {
    return {
      mail: '',
      displayName: '',
      password: '',
      nickName: '',
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
        await this.authStore.register(
          this.mail.trim(),
          this.displayName.trim(),
          this.nickName.trim(),
          this.password,
        )
        await this.$router.replace({ name: 'login', query: { registered: '1' } })
      } catch (e) {
        this.error = e.response?.data?.message || e.message || 'Kayıt başarısız'
      } finally {
        this.loading = false
      }
    },
  },
}
</script>

<template>
  <div class="w-full min-h-screen flex flex-col items-center justify-center px-4 py-12">
    <div class="w-full max-w-md bg-surface/80 backdrop-blur-md rounded-3xl border border-white/10 p-8 shadow-2xl">
      <div class="flex items-center gap-3 mb-8">
        <UserPlus class="w-8 h-8 text-primary" />
        <h1 class="text-2xl font-bold tracking-tight">Kayıt</h1>
      </div>

      <form class="space-y-5" @submit.prevent="submit">
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">E-posta</label>
          <input v-model="mail" type="email" required autocomplete="email"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50" />
        </div>
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">Görünen ad</label>
          <input v-model="displayName" type="text" required maxlength="32" autocomplete="displayName"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50" />
        </div>
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">Oyuncu Adı</label>
          <input v-model="nickName" type="text" required maxlength="32" autocomplete="nickname"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50" />
        </div>
        <div>
          <label class="block text-sm text-text-light/70 mb-1.5">Şifre</label>
          <input v-model="password" type="password" required autocomplete="new-password"
            class="w-full rounded-xl bg-bg-dark/80 border border-white/10 px-4 py-3 text-text-light focus:outline-none focus:ring-2 focus:ring-primary/50" />
        </div>

        <p v-if="error" class="text-sm text-danger">{{ error }}</p>

        <button type="submit" :disabled="loading"
          class="w-full py-3 rounded-xl bg-primary hover:bg-primary/90 text-text-light font-semibold transition disabled:opacity-50">
          {{ loading ? '…' : 'Kayıt ol' }}
        </button>
      </form>

      <p class="mt-6 text-center text-sm text-text-light/60">
        Zaten hesabın var mı?
        <router-link to="/login" class="text-primary hover:underline">Giriş yap</router-link>
      </p>
    </div>
  </div>
</template>
