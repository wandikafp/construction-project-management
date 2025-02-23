<template>
  <div class="overflow-x-auto p-4 shadow-lg rounded-lg bg-white">
    <table class="table table-auto w-full border-collapse">
      <thead>
        <tr class="bg-gray-200">
          <th v-for="column in columns" :key="column.key" class="px-4 py-2 border">
            {{ column.label }}
          </th>
          <th v-if="showActions" class="px-4 py-2 border">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in data" :key="row.id" class="hover:bg-gray-100">
          <td v-for="column in columns" :key="column.key" class="px-4 py-2 border">
            {{ row[column.key] }}
          </td>
          <td v-if="showActions" class="px-4 py-2 border">
            <button @click="infoRow(row)" class="btn btn-info btn-sm">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 inline-block" viewBox="0 0 20 20" fill="currentColor">
                <path d="M9 2a7 7 0 100 14A7 7 0 009 2zm1 10H8v-2h2v2zm0-4H8V5h2v3z" />
              </svg> Info
            </button>
            <button @click="updateRow(row)" class="btn btn-primary btn-sm ml-5">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 inline-block" viewBox="0 0 20 20" fill="currentColor">
                <path d="M17.414 2.586a2 2 0 00-2.828 0L7 10.172V13h2.828l7.586-7.586a2 2 0 000-2.828zM4 12v4h4v-1H5v-3H4z" />
              </svg> Update
            </button>
            <button @click="deleteRow(row)" class="btn btn-error btn-sm ml-5">
              <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5 inline-block" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M6 2a1 1 0 00-1 1v1H3a1 1 0 100 2h1v9a2 2 0 002 2h8a2 2 0 002-2V6h1a1 1 0 100-2h-2V3a1 1 0 00-1-1H6zm3 4a1 1 0 012 0v7a1 1 0 11-2 0V6zm4 0a1 1 0 012 0v7a1 1 0 11-2 0V6z" clip-rule="evenodd" />
              </svg> Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
    <Pagination
      :pageIndex="pageIndex"
      :totalPages="totalPages"
      @next-page="nextPage"
      @prev-page="prevPage"
    />
  </div>
</template>

<script>
import Pagination from './Pagination.vue';

export default {
  components: {
    Pagination
  },
  props: {
    data: {
      type: Array,
      required: true
    },
    columns: {
      type: Array,
      required: true
    },
    pagination: {
      type: Object,
      required: true
    },
    showActions: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      pageIndex: this.pagination.pageIndex,
      itemsPerPage: 10
    };
  },
  watch: {
    pagination: {
      immediate: true,
      handler(newVal) {
        this.pageIndex = newVal.pageIndex;
      }
    }
  },
  computed: {
    totalPages() {
      return this.pagination.totalPages;
    }
  },
  methods: {
    nextPage() {
      if (this.pageIndex < this.totalPages) {
        this.pageIndex++;
        this.$emit('page-changed', this.pageIndex);
      }
    },
    prevPage() {
      if (this.pageIndex > 1) {
        this.pageIndex--;
        this.$emit('page-changed', this.pageIndex);
      }
    },
    updateRow(row) {
      this.$emit('update-row', row);
    },
    deleteRow(row) {
      this.$emit('delete-row', row);
    },
    infoRow(row) {
      this.$emit('info-row', row);
    }
  }
};
</script>

<style>
</style>
