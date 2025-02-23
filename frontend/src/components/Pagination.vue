<template>
  <div class="join flex justify-center mt-8 mb-4">
    <button class="join-item btn" @click="prevPage" :disabled="pageIndex === 1">Previous</button>
    <button class="join-item btn">Page {{ pageIndex }} of {{ totalPages }}</button>
    <button class="join-item btn" @click="nextPage" :disabled="pageIndex === totalPages">Next</button>
  </div>
</template>

<script>
export default {
  props: {
    pageIndex: {
      type: Number,
      required: true
    },
    totalPages: {
      type: Number,
      required: true
    }
  },
  methods: {
    nextPage() {
      this.$emit('next-page');
    },
    prevPage() {
      this.$emit('prev-page');
    }
  }
};
</script>
