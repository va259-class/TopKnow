import { defineStore } from 'pinia'
import * as signalR from '@microsoft/signalr'
import { getApiBaseUrl } from '@/config'
import { useAuthStore } from '@/stores/auth'
import { useQuizStore } from '@/stores/quiz'
import { registerHubListeners } from './helpers/registerHubListeners'

export const useHubStore = defineStore('hub', {
  state: () => ({
    connection: null,
    lobbyUserCount: 0,
    joined: false,
    opponents: [],
    opponent: {
      id: null,
      displayName: null,
    },
    opponentReady: false,
  }),
  actions: {
    async connect() {
      const auth = useAuthStore()
      if (!auth.token) {
        throw new Error('SignalR için oturum açmanız gerekir.')
      }

      await this.disconnect()

      const url = `${getApiBaseUrl()}/gh`
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(url, {
          accessTokenFactory: () => auth.token,
        })
        .withAutomaticReconnect()
        .build()
      registerHubListeners(this.connection, this)
      await this.connection.start()
    },

    async disconnect() {
      if (this.connection) {
        try {
          await this.connection.stop()
        } catch {
          /* ignore */
        }
        this.connection = null
        this.lobbyUserCount = 0
      }
    },

    join() {
      if (this.connection) {
        this.connection.invoke('Join')
      }
    },

    lobbyUserCountChanged(count) {
      this.lobbyUserCount = count
    },

    joinedToLobby() {
      this.joined = true
    },

    opponentsAssigned(players) {
      this.opponents = players
    },
    clearOpponents() {
      this.opponents = []
    },
    askForChallenge(id) {
      this.connection.invoke('AskForChallenge', id)
    },
    challengeRequested(id, displayName) {
      this.opponent.id = id
      this.opponent.displayName = displayName
      this.opponentReady = true
    },
    acceptChallenge() {
      this.connection.invoke('AcceptChallenge', this.opponent.id)
    },
    rejectChallenge() {
      this.opponentReady = false
      this.opponent.id = null
      this.opponent.displayName = null
    },
    async gameStarted(id) {
      const { default: router } = await import('@/router')
      router.push({ name: 'quiz', params: { id: id } })
    },
    isReady(id) {
      this.connection.invoke('UserIsReady', id)
    },
    async loadQuestion(id) {
      const quiz = useQuizStore()
      await quiz.fetchQuestion(id)
      quiz.startGame()
    },
  },
})
