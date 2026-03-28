import { defineStore } from 'pinia'
import * as signalR from '@microsoft/signalr'
import { registerHubListeners } from './helpers/registerHubListeners'

export const useHubStore = defineStore('hub', {
  state: () => ({
    connection: null,
    lobbyUserCount: 0,
  }),
  actions: {
    async connect() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl('https://192.168.254.24:10500/gh')
        .withAutomaticReconnect()
        .build()
      registerHubListeners(this.connection, this)
      await this.connection.start()
    },

    join() {
      this.connection.invoke('Join')
    },

    lobbyUserCountChanged(count) {
      this.lobbyUserCount = count
      console.log(this.lobbyUserCount)
    },
  },
})
