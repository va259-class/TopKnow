<script>
import { mapStores } from 'pinia'
import { useQuizStore } from '@/stores/quiz'
import { useHubStore } from '@/stores/hub'
import { Loader2 } from 'lucide-vue-next'

export default {
  name: 'LobbyView',
  components: {
    Loader2,
  },
  data() {
    return {
      isLoading: true,
      waitingForOpponent: false,
      showChallengeRequest: false
    }
  },
  computed: {
    ...mapStores(useQuizStore, useHubStore),
  },
  async mounted() {
    this.hubStore.clearOpponents();
    this.hubStore.join();
  },
  methods: {
    askForChallenge(id) {
      if (this.waitingForOpponent) {
        return;
      }
      this.hubStore.askForChallenge(id);
      this.waitingForOpponent = true;
    }
  },
  watch: {
    'hubStore.opponentReady': function (n, o) {
      if (n === true) {
        this.showChallengeRequest = true;
        let audio = new Audio("/sounds/newrequest.wav");
        audio.play();
      }
    }
  }
}
</script>

<template>
  <div class="min-h-screen flex flex-col items-center justify-center p-4">
    <div
      class="max-w-md w-full bg-surface/80 backdrop-blur-md rounded-3xl border border-white/10 p-10 text-center shadow-2xl relative overflow-hidden">
      <!-- Background subtle animated pulse -->
      <div
        class="absolute inset-0 bg-gradient-to-br from-primary/10 to-success/10 animate-[pulse_2s_ease-in-out_infinite]">
      </div>
      <transition name="bounce">
        <div class="new-request" v-if="showChallengeRequest">
          <div class="mb-5">
            <span>{{ hubStore.opponent.displayName }} sana meydan okuyor!</span>
          </div>
          <button class="p-3 mr-3 rounded-xl bg-primary hover:bg-primary/90 text-text-light">Kabul Et</button>
          <button class="p-3 rounded-xl bg-danger hover:bg-primary/90 text-text-light">Reddet</button>
        </div>
      </transition>
      <div class="relative z-10">
        <h2 v-if="!hubStore.joined" class="text-3xl font-bold mb-8 text-text-light drop-shadow-md">
          Arenaya Bağlanıyorsunuz
        </h2>
        <h2 v-else class="text-3xl font-bold mb-8 text-text-light drop-shadow-md text-success">
          Arenadasınız 💕
        </h2>

        <div v-if="!hubStore.joined" class="flex justify-center items-center mb-8 h-24">
          <Loader2 class="w-16 h-16 text-primary animate-spin filter drop-shadow-[0_0_10px_rgba(99,102,241,0.8)]" />
        </div>

        <div class="space-y-3">
          <p v-if="waitingForOpponent" class="text-text-light/60 font-medium animate-pulse text-lg mt-4">Cevap
            Bekleniyor</p>
          <p v-else class="text-text-light/60 font-medium animate-pulse text-lg mt-4">Rakibinizi Seçin</p>
        </div>

        <div v-if="hubStore.opponents.length > 0" class="opponents" :class="{ waiting: waitingForOpponent }">
          <ul>
            <li v-for="user in hubStore.opponents" :key="user.id" @click="() => askForChallenge(user.id)">
              <div>
                <span>{{ user.displayName }}</span>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.opponents {
  text-align: left;
  padding: 5px;
  border-radius: 5px;
}

.opponents.waiting {
  background-color: #5f79ad;
  color: #333;
}

.opponents ul {
  list-style: none;

}

.opponents ul>li {
  border: 1px solid #bbb;
  border-radius: 5px;
  margin: 5px;
  padding: 5px;
}

.opponents ul>li:hover {
  cursor: pointer;
  background-color: #5f79ad;
}

.new-request {
  border: 3px solid #ffde20;
  background-color: #1e0d27;
  border-radius: 10px;
  padding: 10px;
  margin-bottom: 30px;
  animation: pulseBorder 1.5s infinite;
}

.bounce-enter-active {
  animation: bounceIn 0.5s;
}

@keyframes bounceIn {
  0% {
    transform: scale(0.3);
    opacity: 0;
  }

  50% {
    transform: scale(1.1);
  }

  70% {
    transform: scale(0.9);
  }

  100% {
    transform: scale(1);
    opacity: 1;
  }
}

@keyframes pulseBorder {
  0% {
    box-shadow: 0 0 0 0 #ffde20;
  }

  70% {
    box-shadow: 0 0 0 10px #f3a719;
  }

  100% {
    box-shadow: 0 0 0 0 #cbf511;
  }
}
</style>