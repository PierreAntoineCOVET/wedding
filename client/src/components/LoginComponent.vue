<template>
  <div v-if="!loggedUser" class="loggin-group">
    <input type="text" class="form-control" v-model="loginText" @keyup.enter="authenticate()" :placeholder="$t('login.userNamePlaceHolder')" />
    <input type="button" :value="$t('login.login')" @click="authenticate()" class="logging-button btn btn-primary" />
  </div>

  <div v-else>
    {{ $t('login.loginAs') }} {{ loggedUser.userName }}
    <input type="button" @click="unconnect()" :value="$t('login.logout')" class="logging-button btn btn-primary" />
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';
  import { useRouter } from 'vue-router'

  import { AuthenticationService } from '../services/AuthenticationService';
  import { type User } from '../models/User'

  const authenticationService = new AuthenticationService();

  const loginText = ref<string | null>(null);
  const loggedUser = ref<User | null>(null);

  const router = useRouter();
  const eventBus = inject('eventBus') as any;

  onMounted(async () => {
    await router.isReady();

    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);

    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }
  })

  async function authenticate() {
    if (!loginText.value) {
      return;
    }

    const user = await authenticationService.authenticate(loginText.value);

    if (user && user.id) {
      console.log(user);
      loggedUser.value = user;
      localStorage.setItem(AuthenticationService.localeStorageKey, JSON.stringify(user));

      eventBus.emit('loggin', user);

    }
    else {
      alert('Code incorect');
    }
  }

  function unconnect() {
    loggedUser.value = null;
    loginText.value = null;
    localStorage.removeItem(AuthenticationService.localeStorageKey);

    eventBus.emit('loggin', null);
  }
</script>

<style scoped>
  .loggin-group {
      display: flex;
  }

  .logging-button {
    margin-left: 1rem;
  }

  .btn,
  .btn:hover {
    color: #fff;
  }
</style>
