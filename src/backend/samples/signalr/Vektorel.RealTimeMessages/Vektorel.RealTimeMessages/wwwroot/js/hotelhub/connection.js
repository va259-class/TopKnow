const hotelHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/hub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
async function start() {
    try {
        await hotelHubConnection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

hotelHubConnection.onclose(async () => {
    await start();
});

start();