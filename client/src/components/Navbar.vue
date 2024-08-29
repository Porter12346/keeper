<script setup>
import { computed, onMounted, ref } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';
import { useRoute } from 'vue-router';
import { AppState } from '@/AppState.js';

const theme = ref(loadState('theme') || 'light')

const route = useRoute()

const routeName = computed(() => route.name)

const account = computed(() => AppState.account)

onMounted(() => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
})
</script>

<template>
  <div class="container-fluid bg-light pb-2">
    <div class="row">
      <div class="col-4 d-flex align-items-center">
        <div class="dropdown">
          <button v-if="routeName == 'Home' && account" class="btn dropdown-toggle" type="button"
            data-bs-toggle="dropdown" aria-expanded="false">
            Create
          </button>
          <ul class="dropdown-menu p-0">
            <li><button data-bs-toggle="modal" data-bs-target="#keepForm"
                class="dropdown-item selectable border-bottom rounded-top py-3">New Keep</button></li>
            <li><button data-bs-toggle="modal" data-bs-target="#vaultForm"
                class="dropdown-item selectable border-top rounded-bottom py-3">New Vault</button></li>
          </ul>
        </div>
      </div>
      <div class="col-4 d-flex align-items-center justify-content-center">
        <router-link title="go to home page" class=" text-md-center order-md-1 order-0" :to="{ name: 'Home' }">
      <img height=" 60" src="../assets/img/logo.png" alt="The Keeper Co." title="The Keeper Co.">
    </router-link>
      </div>
      <div class="col-4 d-flex align-items-center justify-content-end">
        <div class="text-end px-3 order-2">
      <Login />
    </div>
      </div>
    </div>
  </div>
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
