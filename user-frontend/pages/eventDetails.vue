<template>
  <v-layout
    column
    justify-center
    align-center>
    <v-btn @click="back" small>Back</v-btn>
         <v-data-table
    :headers="headers"
    :items="events"
    hide-actions
    class="elevation-1"
    disable-initial-sort

  >
  
    <template slot="items" slot-scope="props">
      <td>{{ props.item.name }}</td>
      <td class="text-xs-right">{{ props.item.date }}</td>
      <td class="text-xs-right">{{ props.item.status }}</td>
    </template>
  </v-data-table>
          </v-layout>
  
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data() {
    return {
      headers: [
        { text: 'Event Title', value: 'name' },
        { text: 'Date', value: 'date' },
        { text: 'Signup Status', value: 'status' }
      ],
      events: []
    }
  },
  mounted() {
    this.getData()
  },
  middleware: 'member',
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
  },
  methods: {
    getData: function() {
      $nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + '/api/getSignedUp', {
          user_id: this.$store.state.info.user_id
        })
        .then(response => {
          $nuxt.$loading.finish()
          this.events = response.data.res
        })
        .catch(function(error) {
          console.log(error)
        })
    },
    back: function() {
      this.$router.go(-1)
    }
  }
}
</script>