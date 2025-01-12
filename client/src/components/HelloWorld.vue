<script setup lang="ts">
  defineProps<{
    msg: string
  }>()
</script>

<template>
  <div class="weather-component">
    <h1>Weather forecast</h1>
    <p>This component demonstrates fetching data from the server.</p>

    <div v-if="loading" class="loading">
      Loading...
    </div>

    <div v-if="error">
      {{ error }}
      <br />
      Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
    </div>

    <div v-if="post" class="content">
      <table>
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="forecast in post" :key="forecast.date">
            <td>{{ forecast.date }}</td>
            <td>{{ forecast.temperatureC }}</td>
            <td>{{ forecast.temperatureF }}</td>
            <td>{{ forecast.summary }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import axios from 'axios';

  type Forecasts = {
    date: string,
    temperatureC: string,
    temperatureF: string,
    summary: string
  }[];

  interface Data {
    loading: boolean,
    post: null | Forecasts,
    error: null | string
  }

  export default defineComponent({
    data(): Data {
      return {
        loading: false,
        post: null,
        error: null
      };
    },
    async created() {
      // fetch the data when the view is created and the data is
      // already being observed
      await this.fetchData();
    },
    //watch: {
    //  // call again the method if the route changes
    //  '$route': 'fetchData'
    //},
    methods: {
      async fetchData() {
        this.loading = true;
        axios
          .get('http://localhost:5000/weatherforecast/')
          .then(response => {
            console.log(response);
            this.post = response.data;
          })
          .catch(error => {
            console.log(error);
            this.error = error;
          })
          .finally(() => this.loading = false);
      }
    },
  });
</script>

<style scoped>
  h1 {
    font-weight: 500;
    font-size: 2.6rem;
    position: relative;
    top: -10px;
  }

  h3 {
    font-size: 1.2rem;
  }

  .greetings h1,
  .greetings h3 {
    text-align: center;
  }

  @media (min-width: 1024px) {
    .greetings h1,
    .greetings h3 {
      text-align: left;
    }
  }
</style>
