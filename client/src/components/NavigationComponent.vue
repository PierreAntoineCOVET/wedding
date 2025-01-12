<template>
  <nav class="navigation">
    <ul class="nav nav-pills">
      <li class="nav-item">
        <RouterLink class="nav-link"
                    :class="{'active': activePage == ActivePage.Home}"
                    to="/"
                    @Click="navigate(ActivePage.Home)">
          Accueil
        </RouterLink>
      </li>
      <li class="nav-item">
        <RouterLink class="nav-link"
                    :class="{'active': activePage == ActivePage.About}"
                    to="/about"
                    @Click="navigate(ActivePage.About)">
          A propos
        </RouterLink>
      </li>
      <li class="nav-item">
        <RouterLink class="nav-link"
                    :class="{'active': activePage == ActivePage.Full}"
                    v-if="loggedUser && loggedUser.accessGroup == AccessType.full"
                    to="/full"
                    @Click="navigate(ActivePage.Full)">
          Custom Full
        </RouterLink>
      </li>
    </ul>
    <Loggin />
  </nav>
</template>

<script setup lang="ts">
  enum ActivePage {
    Home,
    About,
    Full
  }

  import { ref, onMounted, inject } from 'vue';
  import { useRouter, useRoute } from 'vue-router'

  import Loggin from './LogginComponent.vue'
  import { AccessType } from '../models/user'
  import { AuthenticationService } from '../services/AuthenticationService';

  const router = useRouter();
  const route = useRoute();

  const loggedUser = ref<User | undefined>();
  const activePage = ref<ActivePage>();
  const eventBus = inject('eventBus');



  onMounted(async () => {
    await router.isReady();

    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);

    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }

    eventBus.on('loggin', (user) => {
      loggedUser.value = user;

      if(user === null && activePage.value == ActivePage.Full) {
        activePage.value = ActivePage.Home;
        router.push('/');
      }
    });

    activePage.value = getActivePageFromRouteName(route.fullPath);
  })

  function navigate(activePage: ActivePage) {
    this.activePage = activePage;
  }

  function getActivePageFromRouteName(path: string): ActivePage {
    switch (path) {
      case '/':
        return ActivePage.Home;
      case '/about':
        return ActivePage.About;
      case '/full':
        return ActivePage.Full;
      default:
        return ActivePage.Home;
    }
  }
</script>

<style scoped>
  .navigation {
    padding: 1rem 0.5rem;
    background-color: #d9b0b6;
    border-radius: 15px;
    backdrop-filter: blur(5px);
    display: flex;
    justify-content: space-between;
    margin-bottom: 1.5rem;
    position: sticky;
    top: 1rem;
  }
</style>
