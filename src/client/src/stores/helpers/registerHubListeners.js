export function registerHubListeners(connection, store) {
  connection.on('LobbyChanged', (count) => {
    store.lobbyUserCountChanged(count)
  })

  connection.on('Joined', () => {
    store.joinedToLobby()
  })

  connection.on('OpponentsAssigned', (players) => {
    store.opponentsAssigned(players)
  })

  connection.on('ChallengeRequested', (id, displayName) => {
    store.challengeRequested(id, displayName)
  })

  connection.on('GameStarted', (id) => {
    store.gameStarted(id)
  })

  connection.on('LoadQuestion', (id) => {
    store.loadQuestion(id)
  })
}
