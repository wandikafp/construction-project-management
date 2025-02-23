<template>
  <div class="flex justify-center items-center min-h-screen">
    <div class="w-3/4 max-w-md">
      <h2 class="text-2xl font-bold mb-6 text-center">Login</h2>
      <form @submit.prevent="handleSubmit" class="space-y-4">
        <div>
          <label class="input input-bordered flex items-center gap-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              class="h-4 w-4 opacity-70">
              <path
                d="M2.5 3A1.5 1.5 0 0 0 1 4.5v.793c.026.009.051.02.076.032L7.674 8.51c.206.1.446.1.652 0l6.598-3.185A.755.755 0 0 1 15 5.293V4.5A1.5 1.5 0 0 0 13.5 3h-11Z" />
              <path
                d="M15 6.954 8.978 9.86a2.25 2.25 0 0 1-1.956 0L1 6.954V11.5A1.5 1.5 0 0 0 2.5 13h11a1.5 1.5 0 0 0 1.5-1.5V6.954Z" />
            </svg>
            <input type="email" v-model="email" class="grow" placeholder="Email" required />
          </label>
        </div>
        <div>
          <label class="input input-bordered flex items-center gap-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              class="h-4 w-4 opacity-70">
              <path
                fill-rule="evenodd"
                d="M14 6a4 4 0 0 1-4.899 3.899l-1.955 1.955a.5.5 0 0 1-.353.146H5v1.5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-2.293a.5.5 0 0 1 .146-.353l3.955-3.955A4 4 0 1 1 14 6Zm-4-2a.75.75 0 0 0 0 1.5.5.5 0 0 1 .5.5.75.75 0 0 0 1.5 0 2 2 0 0 0-2-2Z"
                clip-rule="evenodd" />
            </svg>
            <input type="password" v-model="password" class="grow" placeholder="Password" required />
          </label>
        </div>
        <div>
          <button type="submit" class="btn btn-primary w-full">
            <span v-if="isLoading" class="loader border-4 border-t-4 border-t-primary border-gray-200 rounded-full w-6 h-6 mr-2 animate-spin"></span>
            Login
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>

import { mapGetters, mapActions } from 'vuex';

export default {
  data() {
    return {
      email: '',
      password: '',
      isLoading: false
    };
  },
  computed: {
    ...mapGetters(['getToken']),
    token() {
      return this.getToken;
    }
  },
  methods: {
    ...mapActions(['login']),
    async handleSubmit() {
      this.isLoading = true;
      await this.login({ email: this.email, password: this.password });
      this.isLoading = false;
      this.$router.push({ name: 'ProjectList' });
    }
  }
};
</script>

<style scoped>
/* Add your styles here */
</style>
