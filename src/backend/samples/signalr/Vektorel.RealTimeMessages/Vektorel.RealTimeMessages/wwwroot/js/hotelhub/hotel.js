const { createApp } = Vue;
const emptyRoomColor = "#a7e6f8";
const filledRoomColor = "#9900dd";
createApp({
    data() {
        return {
            isDayLight: true,
            nickname: null,
            rooms: [
                { number: 1, guest: null, isAvailable: true },
                { number: 2, guest: null, isAvailable: true },
                { number: 3, guest: null, isAvailable: true },
                { number: 4, guest: null, isAvailable: true },
                { number: 5, guest: null, isAvailable: true },
                { number: 6, guest: null, isAvailable: true }
            ]
        }
    },
    mounted() {
        hotelHubConnection.on("ChangeModeToDayLight", () => {
            this.isDayLight = true;
        });

        hotelHubConnection.on("ChangeModeToNight", () => {
            this.isDayLight = false;
        });

        hotelHubConnection.on("CheckIn", (number) => {
            let room = this.rooms.find(f => f.number == number);
            room.isAvailable = false;
        });

        hotelHubConnection.on("CheckOut", (number) => {
            let room = this.rooms.find(f => f.number == number);
            room.isAvailable = true;
        });
    },
    methods: {
        async toggleTime() {
            await hotelHubConnection.invoke("SetMode", !this.isDayLight);
        },
        getColor(number) {
            return this.rooms.find(f => f.number == number).isAvailable ? emptyRoomColor : filledRoomColor;
        },
        changeAvailability(number) {
            if (!this.nickname) {
                return;
            }
            let room = this.rooms.find(f => f.number == number);
            if (!room) {
                return;
            }

            if (room.isAvailable) {
                hotelHubConnection.invoke("CheckInRoom", number, this.nickname);
            }
            else {
                hotelHubConnection.invoke("CheckOutRoom", number, this.nickname);
            }
        }
    },
    computed: {
        containerClass() {
            return this.isDayLight ? "day-light" : "night-time";
        },
        switchButtonClass() {
            return this.isDayLight ? "btn-secondary" : "btn-warning";
        },
        switchButtonText() {
            return this.isDayLight ? "Gece" : "Gündüz";
        }
    }
}).mount('#hotel-application')