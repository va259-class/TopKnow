import axios from 'axios'
import { getApiBaseUrl } from '@/config'
import { useAuthStore } from '@/stores/auth'

const apiClient = axios.create({
  baseURL: getApiBaseUrl(),
  headers: {
    'Content-Type': 'application/json',
  },
})

apiClient.interceptors.request.use((config) => {
  const auth = useAuthStore()
  if (auth.token) {
    config.headers.Authorization = `Bearer ${auth.token}`
  }
  return config
})

apiClient.interceptors.response.use(
  (r) => r,
  async (error) => {
    if (error.response?.status === 401) {
      const auth = useAuthStore()
      await auth.logout()
      const { default: router } = await import('@/router')
      if (router.currentRoute.value.meta.requiresAuth) {
        router.push({ name: 'login', query: { redirect: router.currentRoute.value.fullPath } })
      }
    }
    return Promise.reject(error)
  },
)

function unwrapResult(data) {
  if (data?.isSuccess === true) return data.value
  const msg = data?.error?.message || data?.error?.code || 'İstek başarısız'
  throw new Error(msg)
}

export default {
  async login(mail, password) {
    const { data } = await apiClient.post('/api/authentication/login', { mail, password })
    return unwrapResult(data)
  },

  async register(mail, displayName, nickName, password) {
    const { data } = await apiClient.post('/api/authentication/register', {
      mail,
      displayName,
      nickName,
      password,
    })
    return unwrapResult(data)
  },

  async getQuestion(id) {
    return await apiClient.get('/api/questions/' + id)
  },
}
