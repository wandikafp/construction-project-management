<template>
  <div class="max-w-lg mx-auto p-4 bg-base-100 mt-4">
    <h1 class="text-2xl font-bold mb-4">Project Detail</h1>
    <div class="card shadow-lg p-4">
      <p><strong>Project Name:</strong> {{ project?.projectName }}</p>
      <p><strong>Location:</strong> {{ project?.projectLocation }}</p>
      <p><strong>Stage:</strong> {{ project?.stage }}</p>
      <p><strong>Category:</strong> {{ project?.category }}</p>
      <p v-if="project?.otherCategory"><strong>Other Category:</strong> {{ project?.otherCategory }}</p>
      <p><strong>Start Date:</strong> {{ project?.startDate }}</p>
      <p><strong>Details:</strong> {{ project?.projectDetails }}</p>
      <p><strong>Creator:</strong> {{ project?.creator?.email }}</p>
      <div class="mt-4 flex space-x-2">
        <button @click="updateProject" class="btn btn-primary">Update</button>
        <button @click="deleteProject" class="btn btn-error">Delete</button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  computed: {
    ...mapGetters(['getProjectDetail']),
    project() {
      return this.getProjectDetail;
    }
  },
  methods: {
    ...mapActions(['fetchProjectDetail', 'removeProject']),
    updateProject() {
      this.$router.push(`/project-form/${this.$route.params.id}`);
    },
    async deleteProject() {
      if (confirm('Are you sure you want to delete this project?')) {
        try {
          await this.removeProject({ projectId: this.$route.params.id });
          alert('Project deleted successfully.');
          this.$router.push('/projects');
        } catch (error) {
          console.error('Failed to delete project:', error);
          alert('Failed to delete project.');
        }
      }
    }
  },
  created() {
    this.fetchProjectDetail(this.$route.params.id);
  }
};
</script>
