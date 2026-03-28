import { defineStore } from 'pinia'

const STORAGE_KEY = 'topknow_playground_auth'

function readStored() {
  try {
    const raw = localStorage.getItem(STORAGE_KEY)
    if (!raw) return null
    return JSON.parse(raw)
  } catch {
    return null
  }
}

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: null,
    user: null,
  }),
  getters: {
    isAuthenticated: (state) => Boolean(state.token),
    displayName: (state) => state.user?.displayName ?? '',
  },
  actions: {
    hydrateFromStorage() {
      const data = readStored()
      if (data?.token && data?.user) {
        this.token = data.token
        this.user = data.user
      }
    },

    persist() {
      if (this.token && this.user) {
        localStorage.setItem(
          STORAGE_KEY,
          JSON.stringify({ token: this.token, user: this.user }),
        )
      } else {
        localStorage.removeItem(STORAGE_KEY)
      }
    },

    setSession(token, user) {
      this.token = token
      this.user = user
      this.persist()
    },

    async login(mail, password) {
      const api = (await import('@/services/api')).default
      const data = await api.login(mail, password)
      this.setSession(data.token, {
        id: data.id,
        displayName: data.displayName,
        userType: data.userType,
      })
    },

    async register(mail, displayName, password) {
      const api = (await import('@/services/api')).default
      await api.register(mail, displayName, password)
    },

    async logout() {
      const { useHubStore } = await import('@/stores/hub')
      const hub = useHubStore()
      await hub.disconnect()
      this.token = null
      this.user = null
      this.persist()
    },
  },
})
