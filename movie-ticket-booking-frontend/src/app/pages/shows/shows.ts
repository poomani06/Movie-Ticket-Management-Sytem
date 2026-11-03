import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { ShowService } from '../../core/services/show';
import { Show } from '../../models/show.model';

@Component({
  selector: 'app-shows',
  imports: [CommonModule],
  templateUrl: './shows.html',
  styleUrl: './shows.css'
})
export class Shows implements OnInit {

  shows: Show[] = [];
  movieId = 0;
  isLoading = true;
  errorMessage = '';

  constructor(
  private route: ActivatedRoute,
  private showService: ShowService,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {
    this.movieId = Number(this.route.snapshot.paramMap.get('movieId'));

    this.loadShows();
  }

  loadShows(): void {

    this.isLoading = true;
    this.errorMessage = '';

    this.showService.getShowsByMovieId(this.movieId).subscribe({

      next: (data) => {
  console.log('Shows API Response:', data);

  this.shows = data;
  this.isLoading = false;

  console.log('Loading status:', this.isLoading);

  this.cdr.detectChanges();
},

error: (error) => {
  console.error('Error loading shows:', error);

  this.errorMessage = 'Unable to load shows. Please try again.';
  this.isLoading = false;

  this.cdr.detectChanges();
}

    });
  }
}