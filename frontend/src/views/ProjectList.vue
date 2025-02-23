<template>
  <div class="m-5">
    <div class="pt-4 pl-4 flex justify-between items-center">
      <button class="btn btn-primary" @click="redirectToCreateProject">Create Project</button>
      <div class="flex items-center">
        <input type="text" v-model="searchQuery" placeholder="Search projects..." class="input input-bordered mr-2" />
        <button class="btn btn-primary mr-5" @click="triggerSearch">Search</button>
      </div>
    </div>
    <Table 
      :data="projects" 
      :columns="columns"
      :pagination="pagination"
      :showActions=true 
      @info-row="handleInfoRow"
      @update-row="handleUpdateRow" 
      @delete-row="handleDeleteRow" 
      @page-changed="handlePageChanged"
    />
  </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import Table from '@/components/Table.vue';

export default {
  components: {
    Table
  },
  data() {
    return {
      columns: [
        { label: 'ID', key: 'projectId' },
        { label: 'Project Name', key: 'projectName' },
        { label: 'Location', key: 'projectLocation' },
        { label: 'Stage', key: 'stage' },
        { label: 'Category', key: 'category' },
        { label: 'Start Date', key: 'startDate' },
      ],
      projects: [], // Initialize projects as an empty array
      pagination: { pageIndex: 1, totalPages: 1 }, // Initialize pagination with default values
      searchQuery: '', // Add searchQuery to data
    };
  },
  computed: {
    ...mapGetters(['getProjects', 'getProjectPagination']),
    projects() {
      return this.getProjects;
    },
    pagination() {
      return this.getProjectPagination || { pageIndex: 1, totalPages: 1 }; // Fallback to default values if undefined
    },
    filteredProjects() {
      return this.projects.filter(project => 
        project.projectName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    }
  },
  methods: {
    ...mapActions(['fetchProjects', 'removeProject']),
    handleInfoRow(row) {
      this.$router.push({ path: '/projects/' + row.projectId });
    },
    handleUpdateRow(row) {
      this.$router.push({ path: '/project-form/' + row.projectId });
    },
    async handleDeleteRow(row) {
      if (confirm('Are you sure you want to delete this project?')) {
        try {
          await this.removeProject({ projectId : row.projectId });
          alert('Project deleted successfully.');
          this.handlePageChanged(this.pagination.pageIndex);
        } catch (error) {
          console.error('Failed to delete project:', error);
          alert('Failed to delete project.');
        }
      }
    },
    handlePageChanged(newPage) {
      this.fetchProjects({ pageNumber: newPage, pageSize: this.pagination.pageSize, query: this.searchQuery });
    },
    redirectToCreateProject() {
      this.$router.push('/project-form');
    },
    async fetchProjectsWithFallback() {
      try {
        await this.fetchProjects({ query: this.searchQuery });
      } catch (error) {
        console.error('Failed to fetch projects:', error);
        // Handle the error, e.g., show a notification or log the error
      }
    },
    triggerSearch() {
      this.fetchProjectsWithFallback();
    }
  },
  created() {
    this.fetchProjectsWithFallback();
    this.pagination = this.getProjectPagination || { pageIndex: 1, totalPages: 1 };
  }
};
</script>
