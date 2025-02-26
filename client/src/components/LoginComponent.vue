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

  import { UserService } from '../services/UserService'
  import { type User } from '../models/User'

  const userService = new UserService();

  const loginText = ref<string | null>(null);
  const loggedUser = ref<User | null>(null);

  const router = useRouter();
  const eventBus = inject('eventBus') as any;

  onMounted(async () => {
    await router.isReady();

    const loggedUserString = localStorage.getItem(UserService.localeStorageKey);

    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }
  })

  async function authenticate() {
    if (!loginText.value) {
      return;
    }

    const user = await userService.authenticate(loginText.value);

    if (user && user.id) {
      loggedUser.value = user;
      localStorage.setItem(UserService.localeStorageKey, JSON.stringify(user));

      eventBus.emit('loggin', user);

    }
    else {
      alert('Code incorect');
    }
  }

  function unconnect() {
    loggedUser.value = null;
    loginText.value = null;
    localStorage.removeItem(UserService.localeStorageKey);

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
