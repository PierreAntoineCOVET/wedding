import './assets/main.css'

import { createApp } from 'vue'
import mitt from 'mitt'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

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

const vuetify = createVuetify({
  theme: {
    themes: {
      light: {
        dark: false,
        colors: {
          primary: '#d9b0b6',
          secondary: '#669999'
        }
      },
    },
  },
  icons: {
    defaultSet: 'mdi'
  },
  components,
  directives,
})
app.use(vuetify);

app.use(router);
app.mount('#app');
