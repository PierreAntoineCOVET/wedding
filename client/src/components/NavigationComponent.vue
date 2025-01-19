<template>
  <nav class="navbar navbar-expand-md navigation">
    <div class="container-fluid">
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="nav me-auto mb-2 mb-lg-0 nav-pills">
          <li class="nav-item">
            <RouterLink class="nav-link"
                        :class="{'active': activePage == ActivePage.Home}"
                        to="/"
                        @Click="navigate(ActivePage.Home)">
              {{ t('nav.home') }}
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink class="nav-link"
                        :class="{'active': activePage == ActivePage.Schedule}"
                        to="/schedule"
                        @Click="navigate(ActivePage.Schedule)">
              {{ t('nav.schedule') }}
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink class="nav-link"
                        :class="{'active': activePage == ActivePage.Info}"
                        to="/info"
                        @Click="navigate(ActivePage.Info)">
              {{ t('nav.info') }}
            </RouterLink>
          </li>
          <li class="nav-item">
            <RouterLink class="nav-link"
                        :class="{'active': activePage == ActivePage.Form}"
                        to="/form"
                        @Click="navigate(ActivePage.Form)">
              {{ t('nav.form') }}
            </RouterLink>
          </li>
          <li class="nav-item" v-if="loggedUser && loggedUser.accessGroup == AccessGroup.admin">
            <RouterLink class="nav-link"
                        :class="{'active': activePage == ActivePage.Admin}"
                        to="/admin"
                        @Click="navigate(ActivePage.Admin)">
              {{ t('nav.admin') }}
            </RouterLink>
          </li>
        </ul>


        <Login />
      </div>
    </div>
  </nav>
</template>

<script setup lang="ts">
  enum ActivePage {
    Home,
    Schedule,
    Info,
    Form,
    Admin
  }

  import { ref, onMounted, inject } from 'vue';
  import { useRouter, useRoute } from 'vue-router'
  import { useI18n } from 'vue-i18n'

  import Login from './LoginComponent.vue'
  import { type User, AccessGroup } from '../models/user'
  import { AuthenticationService } from '../services/AuthenticationService';

  const router = useRouter();
  const route = useRoute();
  const { t } = useI18n();

  const loggedUser = ref<User | null>();
  const activePage = ref<ActivePage | null>();
  const eventBus = inject('eventBus') as any;



  onMounted(async () => {
    await router.isReady();

    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);

    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }

    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;

      if (user === null && activePage.value == ActivePage.Admin) {
        activePage.value = ActivePage.Home;
        router.push('/');
      }
    });

    activePage.value = getActivePageFromRouteName(route.fullPath);
  })

  function navigate(targetPage: ActivePage) {
    activePage.value = targetPage;
  }

  function getActivePageFromRouteName(path: string): ActivePage | null {
    switch (path) {
      case '/':
        return ActivePage.Home;
      case '/schedule':
        return ActivePage.Schedule;
      case '/info':
        return ActivePage.Info;
      case '/form':
        return ActivePage.Form;
      default:
        return null;
    }
  }
</script>

<style scoped lang="scss">
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

  .navbar-collapse.collapse.show,
  .navbar-collapse.collapsing {
    margin-top: .5rem;

    ul .nav.nav-pills, ul.nav.nav-pills {
      display: flex;
      flex-direction: column;
    }
  }
</style>
