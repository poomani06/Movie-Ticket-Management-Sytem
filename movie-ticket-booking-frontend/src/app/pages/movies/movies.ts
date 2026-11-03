import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

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

  constructor(
  private movieService: MovieService,
  private router: Router,
  private cdr: ChangeDetectorRef
) {}
  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.movieService.getMovies().subscribe({

      next: (data) => {
  console.log('Movies API Response:', data);

  this.movies = data;
  this.isLoading = false;

  this.cdr.detectChanges();

  console.log('Movies loaded:', this.movies.length);
},

      error: (error) => {
        console.error('Error loading movies:', error);
        this.errorMessage = 'Unable to load movies. Please try again.';
        this.isLoading = false;
      }

    });
  }

  // Navigate to the selected movie's shows
  viewShows(movieId: number): void {
    this.router.navigate(['/shows', movieId]);
  }

}