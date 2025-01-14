<template>
  <div class="content">
    <h3>Infos pour TOUT LE MONDE : </h3>

    <p>
      Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla mollis dui at efficitur varius. Integer ut dapibus metus, at efficitur enim.
      Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vivamus rhoncus luctus nisi auctor convallis.
      Fusce sit amet posuere nibh. Aliquam tempus gravida fermentum. Fusce faucibus facilisis elit id iaculis. Vivamus nec purus at lectus interdum fermentum eu ut est.
      Proin scelerisque purus tellus.
    </p>

    <p>
      Maecenas lacinia sed elit sed molestie. Fusce eu congue nisi. Maecenas sit amet massa ut dui mollis tristique vel id tortor. Nullam in sapien hendrerit,
      suscipit urna a, fermentum odio. Sed efficitur elit quis lacus rutrum, sagittis egestas tortor imperdiet. Proin viverra elementum dui, eu viverra tortor facilisis eu.
      Mauris ut velit posuere, euismod leo a, pellentesque mi. Interdum et malesuada fames ac ante ipsum primis in faucibus.
      Integer a eleifend sem. Sed suscipit quam vel nulla sollicitudin, at ultricies sem dapibus. Praesent dapibus, elit semper posuere tempor, velit sapien pulvinar est,
      a vehicula magna diam tincidunt quam. Duis ac tristique mi, quis mattis nisi.
    </p>

    <p>
      Aenean vehicula sapien vitae mollis fringilla. Suspendisse fermentum ullamcorper eros bibendum porta. Vivamus a ante volutpat, efficitur neque non, laoreet mi.
      In convallis lectus libero, sit amet consequat lacus tempor quis. Praesent accumsan hendrerit eros, ac volutpat odio dignissim non. Vivamus mattis aliquet est ac vehicula.
      Etiam varius eros nibh, vitae placerat augue egestas ac. Praesent ullamcorper sapien quis neque dapibus lacinia. Integer lectus elit, ornare non nunc id, sodales volutpat arcu.
      Donec ultricies gravida odio consectetur varius. Etiam in urna vitae enim sagittis blandit. Etiam dapibus sagittis diam sit amet consectetur.
      Morbi metus nulla, suscipit interdum est efficitur, commodo tincidunt justo. Sed ut tortor sed lacus interdum aliquet non sed ligula. Mauris ultrices accumsan ipsum.
    </p>
  </div>

  <HomePartial v-if="loggedUser && loggedUser.accessGroup == AccessGroup.partial" />

  <HomeFull v-if="loggedUser && loggedUser.accessGroup == AccessGroup.full" />
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';
  
  import HomePartial from './HomePartial.vue'
  import HomeFull from './HomeFull.vue'
  import { type User, AccessGroup } from '../models/user'
  import { AuthenticationService } from '../services/AuthenticationService';

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;

  onMounted(() => {
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);
    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }


    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;
    });
  })
</script>

<style scoped>

  .content {
      text-align: left;
  }

  p {
      margin-bottom: 1rem;
  }
</style>
