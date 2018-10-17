<template>
  <v-layout
    column
    justify-center
    align-center>
    <h1>Welcome To {{name}}. Fill in the details below to get started. </h1>
    <v-form ref='info' v-model="valid">
      <v-text-field
      v-model='firstName'
      label="First Name"
      required
    ></v-text-field>
    <v-text-field
      v-model='lastName'
      label="Last Name"
      required
    ></v-text-field>
    <v-select
          v-model="grade"
          v-bind:items="gradeLevels"
          label="Grade Level"
        ></v-select>
    <v-btn
      :disabled="!valid"
      @click="submit"
    >
    Submit
    </v-btn>
  </v-form>
  </v-layout>
</template>

<script>
import axios from 'axios'
import { mapMutations } from 'vuex'
import { mapFields } from 'vuex-map-fields'

export default {
  data: () => {
    return {
      gradeLevels: [9, 10, 11, 12],
      name: process.env.name
    }
  },
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
    ...mapFields(['info.firstName', 'info.lastName', 'info.grade'])
  },
  methods: {
    submit: function() {
      $nuxt.$loading.start()
      axios
        .post(process.env.baseUrl + '/api/getIfCurrent', {
          firstName: this.firstName,
          lastName: this.lastName,
          grade: this.grade,
        })
        .then((response) => {
          $nuxt.$loading.finish()
          if (response.data.res != false) {
            this.$store.commit('updateCurrent', true)
            this.$store.commit('updateUserID', response.data.res)
            this.$router.push('/home')
          } else {
            this.$store.commit('updateCurrent', false)
            this.$router.push('/register')
          }
        })
        .catch(function(error) {
          console.log(error)
        })
    }
  }
}
</script>