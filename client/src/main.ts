import './assets/main.css'
import mitt from 'mitt'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router.ts'


const app = createApp(App);

const eventBus = mitt();
//app.config.globalProperties.eventEmitter = emitter;
app.provide('eventBus', eventBus);

app.use(router);
app.mount('#app');
