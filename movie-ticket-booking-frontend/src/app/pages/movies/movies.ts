import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MovieService } from '../../core/services/movie';
import { Movie } from '../../models/movie.model';

@Component({
  selector: 'app-movies',
  imports: [CommonModule],
  templateUrl: './movies.html',
  styleUrl: './movies.css'
})
export class Movies implements OnInit {

  movies: Movie[] = [];
  isLoading = true;
  errorMessage = '';

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {

    this.isLoading = true;
    this.errorMessage = '';

    this.movieService.getMovies().subscribe({

      next: (data) => {
        this.movies = data;
        this.isLoading = false;
      },

      error: (error) => {
        console.error('Error loading movies:', error);
        this.errorMessage = 'Unable to load movies. Please try again.';
        this.isLoading = false;
      }

    });
  }
}