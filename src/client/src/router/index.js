import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import LandingView from '@/views/LandingView.vue'
import LobbyView from '@/views/LobbyView.vue'
import QuizView from '@/views/QuizView.vue'
import ResultView from '@/views/ResultView.vue'
import ProfileView from '@/views/ProfileView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'landing',
      component: LandingView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/register',
      name: 'register',
      component: RegisterView,
    },
    {
      path: '/lobby',
      name: 'lobby',
      component: LobbyView,
      meta: { requiresAuth: true },
    },
    {
      path: '/quiz',
      name: 'quiz',
      component: QuizView,
      meta: { requiresAuth: true },
    },
    {
      path: '/result',
      name: 'result',
      component: ResultView,
      meta: { requiresAuth: true },
    },
    {
      path: '/profile',
      name: 'profile',
      component: ProfileView,
      meta: { requiresAuth: true },
    },
  ],
})

router.beforeEach((to, from, next) => {
  const auth = useAuthStore()
  if (to.meta.requiresAuth && !auth.isAuthenticated) {
    next({ name: 'login', query: { redirect: to.fullPath } })
    return
  }
  next()
})

export default router
