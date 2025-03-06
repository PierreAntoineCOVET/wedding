import './assets/main.css'

import { createApp } from 'vue'
import mitt from 'mitt'
import * as $ from 'jquery';

import { createI18n } from 'vue-i18n'
import FrLocale from './locale/fr.json'
import EnLocale from './locale/en.json'

import App from './App.vue'
import router from './router.ts'

//declare global {
//  // eslint-disable-next-line @typescript-eslint/no-explicit-any
//  interface Window { $: any; jQuery: any }
//}
//window.$ = $;
//window.jQuery = $;


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
