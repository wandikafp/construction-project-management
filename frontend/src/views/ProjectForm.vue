<template>
  <div class="max-w-lg mx-auto p-4 bg-base-100 shadow-md rounded my-5">
    <h1 class="text-2xl font-bold mb-4">{{ isEditMode ? 'Edit Project' : 'Create Project' }}</h1>
    <form @submit.prevent="handleSubmit">
      <div class="mb-4">
        <label for="projectName" class="block text-gray-700">Project Name:</label>
        <input type="text" v-model="project.projectName" id="projectName" required class="input input-bordered w-full" />
      </div>
      <div class="mb-4">
        <label for="projectLocation" class="block text-gray-700">Project Location:</label>
        <input type="text" v-model="project.projectLocation" id="projectLocation" required class="input input-bordered w-full" />
      </div>
      <div class="mb-4">
        <label for="stage" class="block text-gray-700">Project Stage:</label>
        <select v-model="project.stage" id="stage" required class="select select-bordered w-full">
          <option value="Concept">Concept</option>
          <option value="Design And Documentation">Design And Documentation</option>
          <option value="Pre-Construction">Pre-Construction</option>
          <option value="Construction">Construction</option>
        </select>
      </div>
      <div class="mb-4">
        <label for="category" class="block text-gray-700">Category:</label>
        <select v-model="project.category" id="category" required class="select select-bordered w-full">
          <option value="Education">Education</option>
          <option value="Health">Health</option>
          <option value="Office">Office</option>
          <option value="Others">Others</option>
        </select>
      </div>
      <div v-if="project.category === 'Others'" class="mb-4">
        <label for="otherCategory" class="block text-gray-700">Other Category:</label>
        <input type="text" v-model="project.otherCategory" id="otherCategory" class="input input-bordered w-full" />
      </div>
      <div class="mb-4">
        <label for="startDate" class="block text-gray-700">Start Date:</label>
        <input type="date" v-model="project.startDate" id="startDate" required class="input input-bordered w-full" />
      </div>
      <div class="mb-4">
        <label for="projectDetails" class="block text-gray-700">Project Details:</label>
        <textarea v-model="project.projectDetails" id="projectDetails" required class="textarea textarea-bordered w-full"></textarea>
      </div>
      <button type="submit" class="btn btn-primary w-full" :disabled="isLoading || !creatorId">
        <span v-if="isLoading" class="loader border-4 border-t-4 border-t-primary border-gray-200 rounded-full w-6 h-6 mr-2 animate-spin"></span>
        {{ isEditMode ? 'Update' : 'Create' }}
      </button>
      <p v-if="!creatorId" class="text-red-500 mt-2">You need to login first</p>
    </form>
    <div v-if="errorMessage" class="toast toast-center toast-top" role="alert">
      <span class="alert alert-info">{{ errorMessage }}</span>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  name: 'ProjectForm',
  props: {
    id: {
      type: String,
      default: null
    }
  },
  data() {
    return {
      project: {
        projectId: null,
        projectName: '',
        projectLocation: '',
        stage: '',
        category: '',
        otherCategory: '',
        startDate: '',
        projectDetails: '',
        creatorId: '3ab6dc51-1d1a-4ee8-892f-fbe37d14c38d'
      },
      isEditMode: false,
      isLoading: false,
      errorMessage: '' // Add errorMessage to data
    };
  },
  computed: {
    ...mapGetters(['getProjectDetail', 'getCreatorId']),
    projectDetail() {
      return this.getProjectDetail;
    },
    creatorId() {
      return this.getCreatorId;
    }
  },
  watch: {
    errorMessage(newVal) {
      if (newVal) {
        setTimeout(() => {
          this.errorMessage = '';
        }, 3000); // Clear errorMessage after 3 seconds
      }
    },
    projectDetail: {
      handler(newVal) {
        if (newVal) {
          this.project = newVal;
        }
      },
      immediate: true
    },
    creatorId: {
      handler(newVal) {
        this.project.creatorId = newVal;
      },
      immediate: true
    },
    id: {
      handler(newVal) {
        if (newVal) {
          this.isEditMode = true;
          this.fetchProjectDetail(newVal);
        } else {
          this.resetProjectData();
        }
      },
      immediate: true
    }
  },
  created() {
    if (this.id) {
      this.isEditMode = true;
      this.fetchProjectDetail(this.id);
    } else {
      this.resetProjectData();
    }
  },
  methods: {
    ...mapActions(['fetchProjectDetail', 'addProject', 'updateProject']),
    async handleSubmit() {
      this.isLoading = true;
      this.errorMessage = ''; // Reset errorMessage before submission
      try {
        if (this.isEditMode) {
          await this.updateProject({ project: this.project });
        } else {
          const response = await this.addProject({ project: this.project });
          this.project.projectId = response.projectId; // Ensure projectId is set
        }
        this.$router.push({ name: 'ProjectDetail', params: { id: this.project.projectId } });
      } catch (error) {
        console.error('Error submitting project:', error.response?.data || error.message);
        this.errorMessage = error.response?.data || error.message || 'An error occurred while submitting the project.';
      } finally {
        this.isLoading = false;
      }
    },
    resetProjectData() {
      this.project = {
        projectId: null,
        projectName: '',
        projectLocation: '',
        stage: '',
        category: '',
        otherCategory: '',
        startDate: '',
        projectDetails: '',
        creatorId: this.creatorId
      };
      this.isEditMode = false;
    }
  }
};
</script>

<style scoped>
/* No additional styles needed as Tailwind CSS is used */
</style>
