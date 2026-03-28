import { defineStore } from 'pinia'
import * as signalR from '@microsoft/signalr'
import { getApiBaseUrl } from '@/config'
import { useAuthStore } from '@/stores/auth'
import { registerHubListeners } from './helpers/registerHubListeners'

export const useHubStore = defineStore('hub', {
  state: () => ({
    connection: null,
    lobbyUserCount: 0,
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
  },
})
