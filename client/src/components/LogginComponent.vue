<template>
  <div v-if="!loggedUser">
    <input type="text" v-model="loginText" @keyup.enter="authenticate()" placeholder="Code de connection" />
    <input type="button" value="Connexion" @click="authenticate()" class="logging-button" />
  </div>

  <div v-else>
    Connecté en tant que {{ loggedUser.name }}.
    <input type="button" @click="unconnect()" value="Deconnexion" class="logging-button" />
  </div>
</template>

<script setup lang="ts">
  import { AuthenticationService } from '../services/AuthenticationService';
  import { ref, onMounted, inject } from 'vue';
  import { useRouter } from 'vue-router'

  import { User, AccessType } from '../models/user'

  const authenticationService = new AuthenticationService();

  const loginText = ref("");
  const loggedUser = ref<User | undefined>();

  const router = useRouter();
  const eventBus = inject('eventBus');

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

    if (user) {
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
  .logging-button {
    margin-left: 1rem;
  }
</style>
