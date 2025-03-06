import './assets/main.css'

import { createApp } from 'vue'
import mitt from 'mitt'

import { createI18n } from 'vue-i18n'
import FrLocale from './locale/fr.json'
import EnLocale from './locale/en.json'

import App from './App.vue'
import router from './router.ts'

const app = createApp(App);

const eventBus = mitt();
app.provide('eventBus', eventBus);

const i18n = createI18n({
  legacy: false,
  locale: navigator.language,
  globalInjection: true,
  fallbackLocale: 'en',
  messages: {
    "fr": FrLocale,
    "en": EnLocale
  }
})
app.use(i18n);

app.use(router);
app.mount('#app');
