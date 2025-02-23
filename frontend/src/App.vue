<template>
  <div class="navbar bg-primary text-primary-content">
    <div class="navbar-start">
      <router-link to="/" class="btn btn-ghost">Home</router-link>
      <router-link to="/projects" class="btn btn-ghost">Projects</router-link>
    </div>
    <div class="navbar-end">
      <router-link v-if="!creatorId" to="/login" class="btn btn-ghost">Login</router-link>
      <button v-else @click="logout" class="btn btn-ghost">Logout</button>
    </div>
  </div>
  <router-view></router-view>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  name: 'App',
  computed: {
    ...mapGetters(['getCreatorId']),
    creatorId() {
      return this.getCreatorId;
    }
  },
  methods: {
    ...mapActions(['logout'])
  }
};
</script>
