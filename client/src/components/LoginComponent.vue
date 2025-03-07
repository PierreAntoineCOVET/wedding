<template>
  <div v-if="!loggedUser" class="main-container">
    <div class="hidden-md-and-up loggin-group">
      <v-text-field @keyup.enter="authenticate()"
                    :label="$t('login.userNamePlaceHolder')"
                    min-width="300px"
                    variant="solo-filled"
                    v-model="loginText"
                    hide-details>
      </v-text-field>
      <v-btn @click.native="authenticate()" icon="mdi-login"></v-btn>
    </div>
    <div class="hidden-sm-and-down loggin-group">
      <v-text-field @keyup.enter="authenticate()"
                    :label="$t('login.userNamePlaceHolder')"
                    min-width="300px"
                    variant="solo-filled"
                    v-model="loginText"
                    hide-details >
      </v-text-field>
      <v-btn @click="authenticate()">{{ $t('login.login') }}</v-btn>
    </div>
  </div>

  <div v-else>
    {{ $t('login.loginAs') }} {{ loggedUser.userName }}
    <input type="button" @click="unconnect()" :value="$t('login.logout')" class="logging-button btn btn-primary" />
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';
  import { useRouter } from 'vue-router'
  import { useI18n } from "vue-i18n";

  import { UserService } from '../services/UserService'
  import { type User } from '../models/User'

  const userService = new UserService();

  const loginText = ref<string | null>(null);
  const loggedUser = ref<User | null>(null);

  const router = useRouter();
  const eventBus = inject('eventBus') as any;

  const { t } = useI18n({ useScope: "global" });

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
      alert(t('login.userLogginError'));
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
      height: 90%
  }

  .main-container {
      align-content: center;
  }
</style>
