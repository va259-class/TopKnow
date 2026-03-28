export function registerHubListeners(connection, store) {
  connection.on('LobbyChanged', (count) => {
    store.lobbyUserCountChanged(count)
  })
}
