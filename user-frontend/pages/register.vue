<template>
  <v-layout
    column
    justify-center
    align-center>
    <h1>Looks like you're new here, fill in the fields below. </h1>
    <v-form ref='info' v-model="valid" v-on:submit.prevent="submit">
      <v-text-field
      v-model='phoneNumber'
      label="Phone Number"
      required
    ></v-text-field>
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
    }
  },
  computed: {
    // The `mapFields` function takes an array of
    // field names and generates corresponding
    // computed properties with getter and setter
    // functions for accessing the Vuex store.
    ...mapFields(['info.phoneNumber'])
  },
  methods: {
    submit: function() {
      axios
        .post(process.env.baseUrl + '/api/registerMember', {
          firstName: this.$store.state.info.firstName,
          lastName: this.$store.state.info.lastName,
          grade: this.$store.state.info.grade,
          phoneNumber: this.$store.state.info.phoneNumber
        })
        .then((response) => {
          this.$store.commit("updateCurrent", true);
          this.$store.commit("updateUserID", response.data.res);
          this.$router.push('/home');
        })
        .catch(function(error) {
          console.log(error)
        })
    }
  }
}
</script>