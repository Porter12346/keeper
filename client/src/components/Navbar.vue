<script setup>
import { onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}

</script>

<template>
  <nav class="bg-light px-3 d-flex justify-content-between align-items-center border-bottom mb-md-3 md-0">
    <router-link class="d-flex flex-grow-1 flex-md-grow-0 order-md-0 order-1" :to="{ name: 'Home' }">
      <h5 class="text-success btn text-success lighten-30 selectable text-uppercase px-3 text-center mb-0">Home
      </h5>
    </router-link>
    <router-link class="flex-grow-1 text-md-center order-md-1 order-0" :to="{ name: 'Home' }" >
      <img height=" 60" src="../assets/img/logo.png" alt="The Keeper Co.">
    </router-link>
    <div class="text-end px-3 order-2">
      <Login />
    </div>
  </nav>
</template>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 576px) {
  nav {
    height: 64px;
  }
}
</style>
