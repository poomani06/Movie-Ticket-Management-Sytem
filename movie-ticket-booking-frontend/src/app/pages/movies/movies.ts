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

  constructor(private movieService: MovieService) {}

  ngOnInit(): void {
    this.loadMovies();
  }

  loadMovies(): void {
    this.movieService.getMovies().subscribe({
      next: (data) => {
        this.movies = data;
      },
      error: (error) => {
        console.error('Error loading movies:', error);
      }
    });
  }
}