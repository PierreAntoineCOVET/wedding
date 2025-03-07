<template>
  <v-toolbar class="bg-primary rounded">
    <template v-slot:prepend>
      <v-app-bar-nav-icon id="menu-activator" class="hidden-md-and-up"></v-app-bar-nav-icon>
    </template>

    <v-toolbar-title class="hidden-sm-and-down"><v-btn to="/">{{ $t('nav.home') }}</v-btn></v-toolbar-title>

    <v-toolbar-items class="hidden-sm-and-down">
      <v-btn to="/schedule">{{ $t('nav.schedule') }}</v-btn>
      <v-btn to="/info">{{ $t('nav.info') }}</v-btn>
      <v-btn to="/forms">{{ $t('nav.form') }}</v-btn>
      <v-btn to="/contact">{{ $t('nav.contact') }}</v-btn>
      <v-btn to="/admin">{{ $t('nav.admin') }}</v-btn>
      <v-spacer></v-spacer>
      <Login />
    </v-toolbar-items>

    <v-menu activator="#menu-activator">
      <v-list>
        <v-list-item>
          <v-btn flat to="/">{{ $t('nav.home') }}</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn flat to="/schedule">{{ $t('nav.schedule') }}</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn flat to="/info">{{ $t('nav.info') }}</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn flat to="/forms">{{ $t('nav.form') }}</v-btn>
        </v-list-item>
        <v-list-item>
          <v-btn flat to="/contact">{{ $t('nav.contact') }}</v-btn>
        </v-list-item>
        <v-list-item v-if="loggedUser && loggedUser.role == Roles.admin">
          <v-btn flat to="/admin">{{ $t('nav.admin') }}</v-btn>
        </v-list-item>
      </v-list>
    </v-menu>

    <v-toolbar-items class="hidden-md-and-up">
      <Login />
    </v-toolbar-items>

  </v-toolbar>
</template>

<script setup lang="ts">
  enum ActivePage {
    Home,
    Schedule,
    Info,
    Form,
    Contact,
    Admin
  }

  import { ref, onMounted, inject } from 'vue';
  import { useRouter } from 'vue-router'

  import Login from './LoginComponent.vue'
  import { type User, Roles } from '../models/User'
  import { UserService } from '../services/UserService'

  const router = useRouter();

  const loggedUser = ref<User | null>();
  const activePage = ref<ActivePage | null>();
  const eventBus = inject('eventBus') as any;

  onMounted(async () => {
    await router.isReady();

    const loggedUserString = localStorage.getItem(UserService.localeStorageKey);

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
  })
</script>

<style scoped lang="scss">
/*  .navigation {
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
  }*/
</style>
