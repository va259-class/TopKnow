<template>
  <div>
    <div class="container">
      <div>
        <h3>Şehir Listesi</h3>
        <ul>
          <li>
            <div class="city-container">
              <input v-model="newCityName" />
              <button @click="addNewCity" :disabled="invalidCityName">Ekle</button>
            </div>
          </li>
          <li v-for="city in cities">
            <div class="city-container">
              <span>{{ city.name }}</span>
              <button @click="() => addToVisited(city)">Gezdim</button>
            </div>
          </li>
        </ul>
      </div>
      <div>
        <h3>Görülen Şehirler</h3>
        <ul>
          <li v-for="city in visitedCities">
            <div class="city-container">
              <span>{{ city.name }}</span>
              <button @click="() => removeFromVisited(city)">Çıkar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
    <div>
      <GoogleMap api-key="AIzaSyCZxR8wkyIXtDpKET9WstzbQc61y11Etuw" style="width: 100%; height: 500px" :center="center"
        :zoom="12" map-id="google-map">
        <AdvancedMarker v-for="(marker, index) in markers" :key="index" :options="marker" />
      </GoogleMap>
    </div>
  </div>
</template>

<script>
import { GoogleMap, AdvancedMarker } from 'vue3-google-map'

function createCity(name, lat, long) {
  return { name: name, latitude: lat, longitude: long }
}
export default {
  name: "CityChecker",
  components: {
    GoogleMap,
    AdvancedMarker
  },
  data() {
    return {
      cities: [],
      visitedCities: [],
      newCityName: null,
      center: { lat: 41.0082, lng: 28.9784 }
    }
  },
  beforeMount() {
    this.cities.push(createCity("Ankara", 39.9334, 32.8597));
    this.cities.push(createCity("İstanbul", 41.0082, 28.9784));
    this.cities.push(createCity("İzmir", 38.4237, 27.1428));
    this.cities.push(createCity("Antalya", 36.8969, 30.7133));
    this.cities.push(createCity("Aydın", 37.8560, 27.8416));
    this.cities.push(createCity("Erzurum", 39.9043, 41.2679));
    this.cities.push(createCity("Nevşehir", 38.6247, 34.7239));
    this.cities.push(createCity("Kars", 40.6013, 43.0975));
  },
  methods: {
    addToVisited(city) {
      this.visitedCities.push(city);
      this.cities = this.cities.filter(f => f.name != city.name);
    },
    removeFromVisited(city) {
      this.cities.push(city);
      this.visitedCities = this.visitedCities.filter(f => f.name != city.name);
    },
    addNewCity() {
      this.cities.push(createCity(this.newCityName, 38.467, 39.676));
      this.newCityName = null;
    }
  },
  computed: {
    visitedCount() {
      return this.visitedCities.length;
    },
    invalidCityName() {
      return this.newCityName == null ||
        (this.newCityName != null && this.newCityName.length <= 1);
    },
    markers() {
      return this.visitedCities.map(c => {
        return {
          position: {
            lat: c.latitude,
            lng: c.longitude
          },
          title: this.cities.name
        }
      });
    }
  }
}
</script>



<style scoped>
.container {
  display: flex;
  width: 100%;
  justify-content: space-between;
}

.container>div {
  flex: 1;
  padding: 20px;
}

ul {
  width: 100%;
  list-style: none;
  margin: 0;
  padding: 0;
}

.city-container {
  display: flex;
}

.city-container>span,
.city-container>input {
  flex: 4;
}

.city-container>button {
  flex: 1;
}
</style>
